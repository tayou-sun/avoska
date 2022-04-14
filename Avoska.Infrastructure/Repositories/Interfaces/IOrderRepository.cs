using System.Collections.Generic;

public interface IOrderRepository {
    Order Create(OrderDto product);
    List<OrderDto> GetOrdersByUserPhone(string phone);
}