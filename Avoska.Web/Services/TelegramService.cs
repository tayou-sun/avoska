using System;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

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

    private string GetChangeModeName (int? value) {

        switch (value){
            case 1: return "Не звонить, удалить товар из заказа"; break;
            case 2: return "Не звонить, подобрать замену самостоятельно"; break;
         case 3: return "Позвонить для замены"; break;
        }
        return "";
    }
    public void SendMessage(OrderDto order, int id)
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
        message.AppendLine(String.Format("Комментарий: {0}", order.Comment));




     message.AppendLine();
        message.AppendLine(String.Format("Замена {0}:", GetChangeModeName(order.ChangeMode)));

 message.AppendLine();
 
 if (order.Code != "") {
        message.AppendLine("* * * * * * * * * * * * * *");
        message.AppendLine(String.Format("Промо-код: {0}", order.Code));
        message.AppendLine("* * * * * * * * * * * * * *");
 }

        message.AppendLine("__________________");
        message.AppendLine(String.Format("Ссылка на заказ: https://mgmt.avoska-dostavka.ru/order?id={0}", id));


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
}