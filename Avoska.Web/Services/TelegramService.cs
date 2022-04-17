using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

public class TelegramService
{
    private static TelegramBotClient client;

    public TelegramService()
    {
        client = new TelegramBotClient("1901786898:AAHBiYdaG1zIHH_j7A-ay6TozsI6BwyAOiA");

        //client.OnMessageEdited += BotOnMessageReceived;
        /* 	client.OnMessage += BotOnMessageReceived;
            client.OnMessageEdited += BotOnMessageReceived;
            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();  */
    }

    private string GetChangeModeName(int? value)
    {

        switch (value)
        {
            case 1: return "Не звонить, удалить товар из заказа"; break;
            case 2: return "Не звонить, подобрать замену самостоятельно"; break;
            case 3: return "Позвонить для замены"; break;
        }
        return "";
    }
    public void SendMessage(OrderDto order, Order id)
    {
        var message = new StringBuilder();

        message.AppendLine(String.Format("Имя: {0}", order.Name));
        message.AppendLine(String.Format("Телефон: {0}", order.Phone));
        message.AppendLine(String.Format("Адрес: {0}", order.Address));

        message.AppendLine();

        message.AppendLine("Заказ: ");

        decimal summ = 0;
        var index = 1;

        foreach (var item in order.Products)
        {
            var res = item.Price * item.Count;
            summ += res;
            message.AppendLine(String.Format("{0}. {1} x {2} [{3} руб]", index, item.Name, item.Count, res));
            index++;
        }

        message.AppendLine(String.Format("Итого: {0} руб", summ));

        message.AppendLine();

        message.AppendLine(String.Format(id.User == null ? "Пользователь неавторизован" : "Пользователь авторизован"));
        message.AppendLine();

        message.AppendLine(String.Format("Комментарий: {0}", order.Comment));




        message.AppendLine();
        message.AppendLine(String.Format("Замена {0}:", GetChangeModeName(order.ChangeMode)));

        message.AppendLine();

        if (order.Code != "")
        {
            message.AppendLine("* * * * * * * * * * * * * *");
            message.AppendLine(String.Format("Промо-код: {0}", order.Code));
            message.AppendLine("* * * * * * * * * * * * * *");
        }

        message.AppendLine("__________________");
        message.AppendLine(String.Format("Ссылка на заказ: https://mgmt.avoska-dostavka.ru/order?id={0}", id.Id));


        //var message = "Заказ на адрес " + order.Address;
        client.SendTextMessageAsync("-1001763086620", message.ToString());
    }
    private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
    {
        var message = messageEventArgs.Message;
        if (message?.Type == MessageType.Text)
        {
            await client.SendTextMessageAsync(message.Chat.Id, message.Text);
        }
    }





    public async Task SendMessage1Async()
    {

        using (FileStream stream = System.IO.File.OpenRead("/Users/ekaterina/Desktop/275433383_663320451666254_6513114512797389502_n.jpeg"))
        {
            InputOnlineFile inputOnlineFile = new InputOnlineFile(stream, "l");
            var t = @"Дарите популярные и полезные подарки Вашим защитникам с бесплатной доставкой к Вам или сразу к получателю🚀🎖
А ещё мы привезём всё к праздничному столу 🍫🍰🍕
#Авоська #Доставка #Находка
Дата публикации: 21.02.2022
Оригинал: https://www.instagram.com/p/CaPJDjPs0DP/";

            var client1 = new TelegramBotClient("5262595780:AAHPAsCaeNssoYJpc9ufk4vz83oX-utT1Wc");

            var res = await client1.SendPhotoAsync("-1001727486641", inputOnlineFile, t);

            //var message = "Заказ на адрес " + order.Address;

        }

    }
}