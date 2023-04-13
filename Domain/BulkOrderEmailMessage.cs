namespace LudyCakeShop.Domain
{
    public class BulkOrderEmailMessage : EmailMessage
    {
        public BulkOrderEmailMessage(BulkOrder bulkOrder)
        {
            this.Subject = "New Bulk Order Request from " + bulkOrder.CompanyName;
            this.Content = $@"You have a new bulk order request. Details below: 
            Bulk Order ID: {bulkOrder.BulkOrderID}
            Company Name: {bulkOrder.CompanyName}
            Company Contact Number: {bulkOrder.CompanyContactNumber}
            Company Contact Person: {bulkOrder.CompanyContactPerson}

            Please view detailed request in LudyCakeShop Order Management app.";
        }
    }
}
