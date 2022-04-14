  public static class Vk
  {
    public static void GetCode()
    {
      string reqStrTemplate =
        "http://api.vkontakte.ru/oauth/authorize?client_id={0}&scope=offline,wall";

      System.Diagnostics.Process.Start(
        string.Format(reqStrTemplate, 8101707));

    }
  }
