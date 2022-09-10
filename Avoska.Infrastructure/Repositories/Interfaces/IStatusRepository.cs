using System.Collections.Generic;

public interface IStatusRepository
{
    Order SetStatus(int orderId, int statusId);
    string GetDetailById(string phone);
}