using LudyCakeShop.Domain;
using System.Collections.Generic;
using System;
using System.Linq;

namespace LudyCakeShop.Controllers
{
    public class Mapper
    {
        public static IEnumerable<OrderDTO> MaptoDTOs(IEnumerable<Order> ordersList)
        {
            IEnumerable<OrderDTO> ordersDTOList = ((List<Order>)ordersList).ConvertAll(new Converter<Order, OrderDTO>(MaptoDTO));

            return ordersDTOList;
        }

        public static OrderItem MaptoDomain(OrderItemDTO orderItemDTO)
        {
            OrderItem orderItem = new();
            orderItem.OrderNumber = orderItemDTO.OrderNumber;
            orderItem.ProductID = orderItemDTO.ProductID;
            orderItem.ItemTotal = orderItemDTO.ItemTotal;
            orderItem.ItemQuantity = orderItemDTO.ItemQuantity;

            return orderItem;
        }

        public static OrderItemDTO MaptoDTO(OrderItem orderItem)
        {
            OrderItemDTO orderItemDTO = new();
            orderItemDTO.OrderNumber = orderItem.OrderNumber;
            orderItemDTO.ProductID = orderItem.ProductID;
            orderItemDTO.ItemTotal = orderItem.ItemTotal;
            orderItemDTO.ItemQuantity = orderItem.ItemQuantity;

            return orderItemDTO;
        }

        public static OrderDTO MaptoDTO(Order order)
        {
            OrderDTO orderDTO = new();
            orderDTO.OrderNumber = order.OrderNumber;
            orderDTO.InvoiceNumber = order.InvoiceNumber;
            orderDTO.OrderDate = order.OrderDate;
            orderDTO.OrderStatus = order.OrderStatus;
            orderDTO.GST = order.GST;
            orderDTO.SubTotal = order.SubTotal;
            orderDTO.SaleTotal = order.SaleTotal;
            orderDTO.CustomerName = order.CustomerName;
            orderDTO.CustomerAddress = order.CustomerAddress;
            orderDTO.CustomerEmail = order.CustomerEmail;
            orderDTO.CustomerContactNumber = order.CustomerContactNumber;
            orderDTO.Note = order.Note;
            orderDTO.OrderItems = order.OrderItems.Select(orderItem => MaptoDTO(orderItem));

            return orderDTO;
        }

        public static Order MaptoDomain(OrderDTO orderDTO)
        {
            Order order = new();
            order.OrderNumber = orderDTO.OrderNumber;
            order.InvoiceNumber = orderDTO.InvoiceNumber;
            order.OrderDate = orderDTO.OrderDate;
            order.OrderStatus = orderDTO.OrderStatus;
            order.GST = orderDTO.GST;
            order.SubTotal = orderDTO.SubTotal;
            order.SaleTotal = orderDTO.SaleTotal;
            order.CustomerName = orderDTO.CustomerName;
            order.CustomerAddress = orderDTO.CustomerAddress;
            order.CustomerEmail = orderDTO.CustomerEmail;
            order.CustomerContactNumber = orderDTO.CustomerContactNumber;
            order.Note = orderDTO.Note;
            order.OrderItems = orderDTO.OrderItems.Select(orderItemDTO => MaptoDomain(orderItemDTO));

            return order;
        }

        public static IEnumerable<ProductDTO> MaptoDTOs(IEnumerable<Product> productsList)
        {
            IEnumerable<ProductDTO> productsDTOList = ((List<Product>)productsList).ConvertAll(new Converter<Product, ProductDTO>(MaptoDTO));

            return productsDTOList;
        }

        public static ProductDTO MaptoDTO(Product product)
        {
            ProductDTO productDTO = new();
            productDTO.ProductID = product.ProductID;
            productDTO.ProductName = product.ProductName;
            productDTO.ProductDescription = product.ProductDescription;
            productDTO.QuantityAvailable = product.QuantityAvailable;
            productDTO.UnitPrice = product.UnitPrice;
            productDTO.Discontinued = product.Discontinued;
            productDTO.QuantityPerUnit = product.QuantityPerUnit;
            productDTO.CategoryID = product.CategoryID;
            productDTO.ProductImageID = product.ProductImageID;

            return productDTO;
        }

        public static Product MaptoDomain(ProductDTO productDTO)
        {
            Product product = new();
            product.ProductID = productDTO.ProductID;
            product.ProductName = productDTO.ProductName;
            product.ProductDescription = productDTO.ProductDescription;
            product.QuantityAvailable = productDTO.QuantityAvailable;
            product.UnitPrice = productDTO.UnitPrice;
            product.Discontinued = productDTO.Discontinued;
            product.QuantityPerUnit = productDTO.QuantityPerUnit;
            product.CategoryID = productDTO.CategoryID;
            product.ProductImageID = productDTO.ProductImageID;

            return product;
        }
    }
}
