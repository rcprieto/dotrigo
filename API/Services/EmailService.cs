using System;
using System.Net.Mail;
using System.Text;
using API.Domain.Entidades;

namespace API.Services;

public static class EmailService
{
	public static void EmailPedidoEnviado(Pedido pedido)
	{
		string assuntoMensagem = "dotrigo - Pedido Enviado ";
		StringBuilder corpo = new StringBuilder();
		corpo.Append($@" <tr>
                    <td style='padding-bottom: 30px'>
					ID: {pedido.Id}<br>
					Endereço: {pedido.ClienteEndereco}
                   Cliente: {pedido.ClienteNome}<br>
                   Email: {pedido.ClienteEmail}<br>
                   Telefone: {pedido.ClienteTelefone}<br>
                   Comentários: {pedido.Comentarios}<br>
                   Data: {pedido.DataPedido.ToString("dd/MM/yyyy HH:mm")}<br>
                   Total: R${pedido.ValorTotal}<br>
				   Chave PIX: 0e1e80a5-f2b0-4709-b123-7bf87d2f714d
                    </td>
                </tr>");

		foreach (var item in pedido.Itens)
		{
			corpo.Append($@" <tr>
                    <td style='padding-bottom: 30px'>
                    Produto: {item.NomeProduto}<br>
                    Quantidade: {item.Quantidade}<br>
                    Preço: R${item.PrecoUnitario}<br>
                    </td>
                </tr>");
		}


		string mensagem = ConstroiEmail(assuntoMensagem, corpo.ToString());

		EnviarEmail("rcprieto@gmail.com", assuntoMensagem, mensagem);
	}

	private static void EnviarEmail(string emailDestinatario, string assuntoMensagem, string mensagem)
	{

		//Define os dados do e-mail
		string nomeRemetente = "dotrigo - padaria artesanal";
		string emailRemetente = "suporte@constructoit.com.br";


		//Cria objeto com dados do e-mail.
		MailMessage objEmail = new MailMessage();

		//Define o Campo From e ReplyTo do e-mail.
		objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + emailRemetente + ">");

		//Define os destinatários do e-mail.
		objEmail.To.Add(emailDestinatario);

		objEmail.Priority = System.Net.Mail.MailPriority.Normal;
		objEmail.IsBodyHtml = true;
		objEmail.Subject = assuntoMensagem;
		objEmail.Body = mensagem.ToString();

		//Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
		objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
		objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");


		//Cria objeto com os dados do SMTP
		System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

		objSmtp.Host = "smtp.gmail.com";
		objSmtp.EnableSsl = true;
		objSmtp.Port = 587;
		objSmtp.UseDefaultCredentials = false;
		objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
		objSmtp.Credentials = new System.Net.NetworkCredential("suporte@constructoit.com.br", "kbuv gtsn dnyz ujly");
		objSmtp.Timeout = 20000;
		//Enviamos o e-mail através do método .send()
		try
		{
			objSmtp.Send(objEmail);

		}
		catch (Exception ex)
		{
			string erroMensagem = "Ocorreram problemas no envio do e-mail. Erro = " + ex.Message;

		}
		finally
		{
			//excluímos o objeto de e-mail da memória
			objEmail.Dispose();
			//anexo.Dispose();
		}
	}

	private static string ConstroiEmail(string titulo, string corpo, bool enviarRodape = true)
	{
		StringBuilder email = new StringBuilder();

		email.Append($@"<html>
                            <head>
                            </head>
                            <body style='background-color: #ffffffff; height:600px; padding-top:50px;'>
                            <table style='width: 600px; height: 500px; padding: 0; margin:30px; background-color:white;' cellspacing='0' cellpadding='0'>
                                <tr>
                                    <td style='vertical-align: top; margin: 0; padding: 0; height: 150px;'>
                                        <img height='80px' src='https://dotrigo.citapps.com.br/assets/images/logodotrigo_pq.png' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style=' background-color: white; margin: 0; padding: 20px; vertical-align: top'>
                                        
                                        <table style='color: black; font-family: verdana; vertical-align: top;  width: 100%'>
                                            <tr>
                                                <td>
                                                    <h3>{titulo}</h3>
                                                    <hr />
                                                </td>
                                            </tr>
                                {corpo}");
		if (enviarRodape)
		{
			email.Append($@"<tr>
                                                <td>
                                                    Para acessar a ferramenta <a href='https://dotrigo.citapps.com.br' style='color: #000000ff' target='_blank'>clique aqui</a>
                                                </td>
                                            </tr>");
		}

		email.Append($@"   <tr>
                                <td style='padding-top:20px;'>
                                <i style='font-size: 12px'>*Esse é um e-mail automático, por favor, não responda.  WhatsApp: (11)99102-1598</i>
                                </td>
                                </tr>
                            </table>

                                    </td>
                                </tr>
                            </table>");

		return email.ToString();
	}
}
