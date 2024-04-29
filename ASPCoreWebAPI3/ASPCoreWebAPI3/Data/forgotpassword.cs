//using System.Net.Mail;
//using System.Net;

//public void SendResetPasswordEmail(string email)
//{
//    // Configure SMTP settings
//    var smtpServer = "smtp.example.com"; // Replace with your SMTP server address
//    var smtpPort = 587; // Replace with your SMTP port
//    var smtpUsername = "your_username"; // Replace with your SMTP username
//    var smtpPassword = "your_password"; // Replace with your SMTP password

//    // Create SMTP client
//    using (var client = new SmtpClient(smtpServer, smtpPort))
//    {
//        client.UseDefaultCredentials = false;
//        client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
//        client.EnableSsl = true;

//        // Create email message
//        var mailMessage = new MailMessage
//        {
//            From = new MailAddress("noreply@example.com"), // Replace with your email address
//            Subject = "Password Reset",
//            Body = "Your password has been reset successfully." // You can customize the email body
//        };

//        mailMessage.To.Add(email);

//        // Send email
//        client.Send(mailMessage);
//    }
//}