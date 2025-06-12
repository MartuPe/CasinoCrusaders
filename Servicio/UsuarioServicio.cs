using Entidades;
using Entidades.EF;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using Org.BouncyCastle.Crypto.Macs;
using System.Net.Mail;

namespace Servicio;

public enum EmailVerificationResult { Correcto, TokenInvalido, TokenExpirado }
public enum EmailResendResult { Enviado, UsuarioNoEncontrado, YaVerificado }

public interface IUsuarioServicio
{
    void AgregarUsuario(Usuario usuario);
    Usuario ObtenerUsuarioPorId(int id);
    Usuario ObtenerUsuarioPorEmail(String email);
    List<Usuario> ObtenerTodosLosUsuarios();
    void ActualizarUsuario(Usuario usuario);
    void EliminarUsuario(int id);
    EmailVerificationResult VerificarEmailConToken(string token);
    EmailResendResult ReenviarToken(string gmail);
    Usuario? ValidarLogin(string nombreUsuario, string contraseña);
    bool ValidarSiGmailExiste(string? email);

    bool ExisteOtroUsuarioConEseEmail(int idUsuarioActual, string email);

}
public class UsuarioServicio : IUsuarioServicio
{
    CasinoCrusadersContext _context;
    private readonly PasswordHasher<string> _passwordHasher;


    public UsuarioServicio(CasinoCrusadersContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<string>();
    }
    public void AgregarUsuario(Usuario usuario)
    {
        string token = Guid.NewGuid().ToString("N");
        DateTime expiration = DateTime.UtcNow.AddHours(24);
        string passwordHasheada = _passwordHasher.HashPassword(usuario.NombreUsuario, usuario.Contraseña);

        usuario.TipoUsuario = "Usuario";
        usuario.Contraseña = passwordHasheada;
        usuario.EmailVerificacionToken = token;
        usuario.ExpiracionToken = expiration;

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        EnviarCorreoVerificacion(usuario.Gmail, usuario.EmailVerificacionToken);

    }

    public Usuario? ValidarLogin(string nombreUsuario, string contraseña)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);

        if (usuario == null)
            return null;
      
        var resultado = _passwordHasher.VerifyHashedPassword(nombreUsuario, usuario.Contraseña, contraseña);

        return resultado == PasswordVerificationResult.Success ? usuario : null;
    }

    public bool ValidarSiGmailExiste(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return _context.Usuarios.Any(u => u.Gmail == email && u.EmailVerificado);
    }

    public Usuario ObtenerUsuarioPorId(int id)
    {
       return _context.Usuarios.Find(id);
    }

    public Usuario ObtenerUsuarioPorEmail(string email)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Gmail == email);
    }

    public List<Usuario> ObtenerTodosLosUsuarios()
    {
        return _context.Usuarios.ToList();
    }

    public void ActualizarUsuario(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();
    }

    public void EliminarUsuario(int id)
    {
        var usuario = ObtenerUsuarioPorId(id);
        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();
    }

    private void EnviarCorreoVerificacion(string email, string token)
    {
        string link = $"https://localhost:7000/Usuario/Verificar?token={token}";

        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress("CasinoCrusaders", "marty4009@gmail.com"));
        mensaje.To.Add(MailboxAddress.Parse(email));
        mensaje.Subject = "Verifica tu cuenta";

        mensaje.Body = new TextPart("html")
        {
            Text = $@"
        <h3>¡Gracias por registrarte en CasinoCrusaders!</h3>
        <p>Haz clic en el siguiente botón para verificar tu correo:</p>
        <a href='{link}' 
           style='display: inline-block; padding: 10px 20px; font-size: 16px; 
                  color: white; background-color: #007bff; text-decoration: none; 
                  border-radius: 5px;'>Verifica tu correo</a>
        <p>Este enlace expirará en 24 horas.</p>"
        };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, false);
        smtp.Authenticate("marty4009@gmail.com", "rcre rxfg uakt gbox");
        smtp.Send(mensaje);
        smtp.Disconnect(true);
    }

    public EmailVerificationResult VerificarEmailConToken(string token)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.EmailVerificacionToken == token);
        if (usuario == null)
            return EmailVerificationResult.TokenInvalido;

        if (usuario.ExpiracionToken < DateTime.UtcNow)
            return EmailVerificationResult.TokenExpirado;

        usuario.EmailVerificado = true;
        usuario.EmailVerificacionToken = null;
        usuario.ExpiracionToken = null;
        _context.SaveChanges();

        return EmailVerificationResult.Correcto;
    }

    public EmailResendResult ReenviarToken(string gmail)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Gmail == gmail);
        if (usuario == null)
            return EmailResendResult.UsuarioNoEncontrado;

        if (usuario.EmailVerificado)
            return EmailResendResult.YaVerificado;

        usuario.EmailVerificacionToken = Guid.NewGuid().ToString("N");
        usuario.ExpiracionToken = DateTime.UtcNow.AddHours(24);
        _context.SaveChanges();

        EnviarCorreoVerificacion(gmail, usuario.EmailVerificacionToken);

        return EmailResendResult.Enviado;
    }


    public bool ExisteOtroUsuarioConEseEmail(int idUsuarioActual, string gmail)
    {
        return _context.Usuarios.Any(u => u.Gmail == gmail && u.IdUsuario != idUsuarioActual);
    }


}
