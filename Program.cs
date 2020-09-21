using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;


namespace Email
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute().Wait();
        }
        static async Task Execute()
        {
            var apiKey = "SENDGRID_API_KEY"; 
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("wallace.nascimento@lftm.com.br", "Wallace Brito");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("dragonwits@gmail.com", "Wallace"),
                new EmailAddress("samuel.conceicao@lifetimeinvest.com.br", "Samuel"),
                new EmailAddress("pedro.silva@lftm.com.br", "Pedro")
            };
            
            var templateId = "d-9d2b006a4d354f2ab7438ee69a61782f";
            var dynamicTemplateData = new List<Object>
            {
                new ExampleTemplateData  {
                        Subject = "Hi!",
                        Name = "Wallace",
                        Valor = "500,00"
                    },
                new ExampleTemplateData  {
                    Subject = "Hi!",
                    Name = "Samuel",
                    Valor = "1500,00"
                    
                },
                new ExampleTemplateData  {
                    Subject = "Hi!",
                    Name = "Pedro",
                    Valor = "5000,00"
                }
            };

            var msg = MailHelper.CreateMultipleTemplateEmailsToMultipleRecipients(
                                                                            from,
                                                                            tos,
                                                                            templateId,
                                                                            dynamicTemplateData                                                                            
                                                                            );


            //msg.SetTemplateId("d-9d2b006a4d354f2ab7438ee69a61782f");
            // msg.SetTemplateData(dynamicTemplateData);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());

        }

        private class ExampleTemplateData
        {
            [JsonProperty("subject")]
            public string Subject { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("valor")]
            public string Valor { get; set; }

            [JsonProperty("location")]
            public Location Location { get; set; }
        }

        private class Location
        {
            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }
        }
    }
}
