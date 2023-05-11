using LudyCakeShop.Core.Domain.Data;

namespace LudyCakeShop.Core.Domain.Notification
{
    public class OrderEmailMessage : EmailMessage
    {
        public OrderEmailMessage(Order order)
        {
            this.Subject = "New Order Received from " + order.CustomerContactNumber;
            this.Content = $@"You have a new order. Details as follows: 
            Order ID: {order.OrderID}
            Customer Name: {order.CustomerName}
            Customer Contact: {order.CustomerContactNumber}
            Order Total: CA${order.SaleTotal}

            Please view detailed order in LudyCakeShop Order Management app.";
        }
    }
}
