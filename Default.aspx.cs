using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SendEmail(object sender, EventArgs e)
    {
 
        string content = txtBody.Text;       
        MailMessage message = new MailMessage();
        message.To.Add(new MailAddress(txtTo.Text));
        message.From =new MailAddress(txtEmail.Text);
        message.Subject = txtSubject.Text.ToString();
        if (fuAttachment.HasFile)
        {
            string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
            message.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
        }
        message.BodyTransferEncoding = TransferEncoding.QuotedPrintable;
        message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("this is text", new ContentType("text/plain")));
        message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("<b>this is html</b>", new ContentType("text/html")));
        message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(txtBody.Text, System.Text.Encoding.UTF8, "text/x-amp-html"));
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            DeliveryFormat = SmtpDeliveryFormat.International,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(txtEmail.Text, txtPassword.Text)
        };

        client.Send(message);
        

    }
}
