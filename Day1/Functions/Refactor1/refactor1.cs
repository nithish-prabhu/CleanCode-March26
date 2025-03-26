public void ProcessOrder(Order order) 
{
    // Validate order
    ValidateOrder(order);
    
    // Calculate total
    decimal total = CalculateTotal( order, 0.1 )

    // Apply discount
    var TotalAfterDiscount = computeDiscount(order, total);

    // Save to DB
    SaveOrder(order, TotalAfterDiscount);

    // Send confirmation email
    sendConfirmationEmail(order, TotalAfterDiscount);
   
}

public void ValidateOrder(Order order) 
{
    if (order == null) throw new ArgumentNullException();
    if (order.Items.Count == 0) throw new InvalidOperationException("Empty order");
}

public decimal CalculateTotal(Order order, float taxRate){
    decimal total = 0;
    foreach (var item in order.Items) 
    {
        total += item.Price * item.Quantity;
        if (item.IsTaxable) 
        {
            total += item.Price * taxRate; 
        }
    }
    return total;
}

public computeDiscount(Order order, decimal total)
{
    // Calculate discount
    if (order.Customer.IsPremium) 
    {
         total *= 0.9m; // 10% discount
    }
    return total;
}

public void SaveOrder(Order order, decimal total) 
{
    var db = new Database();
    db.Save(order, total);
}

public void sendConfirmationEmail(Order order, decimal total) 
{
    var email = new Email();
    email.Send(order.Customer.Email, "Order Confirmation", $"Thank you for your order. Total: {total}");
}