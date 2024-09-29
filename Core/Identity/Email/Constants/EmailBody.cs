namespace CleanBase.Core.Identity.Email.Constants;

public static class EmailBody
{
    public static string Verification(string fullName, string url)
    {
        return @$"
                <p>Dear {fullName},</p>
                <p>Congratulations and welcome to Template! This email serves as confirmation that your registration has been successfully completed.  We're thrilled to have you as a part of our community and look forward to providing you with a rewarding experience.</p>
                <a href={url}>Complete the Registration process by clicking the link</a>
                <br/>
                <p>Best Regards,</p>
                <p>Your Template Team</p>";
    }

    public static string PasswordReset(string fullName, string url)
    {
        return @$"
                <p>Dear {fullName},</p>
                <p>We have received a request to reset your password for your account. To ensure the security of your account, please follow the instructions below:</p>
                <ul>
                    <li>Click on the following link to <a href={url}>reset your password</a></li>
                    <li>You will be redirected to a secure page where you can enter your new password</li>
                    <li>Choose a strong, unique password that you haven't used before and is not easily guessable.</li>
                </ul>
                <p>Best Regards,</p>
                <p>Your Template Team</p>";
    }

    public static string Ordered(
        string fullName, 
        string orderCode, 
        string totalPrice, 
        string recipientName, 
        string orderStatus,
        string recipientEmail,
        string recipientPhone,
        string address
        )
    {
        return @$"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    color: #333;
                    margin: 0;
                    padding: 20px;
                }}
                .container {{
                    max-width: 600px;
                    margin: auto;
                    background: #ffffff;
                    border-radius: 8px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    padding: 20px;
                }}
                h3 {{
                    color: #007BFF;
                }}
                ul {{
                    list-style-type: none;
                    padding: 0;
                }}
                li {{
                    padding: 10px 0;
                    border-bottom: 1px solid #eee;
                }}
                li:last-child {{
                    border-bottom: none;
                }}
                strong {{
                    color: #555;
                }}
                .footer {{
                    margin-top: 20px;
                    font-size: 12px;
                    color: #777;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <p>Dear {fullName},</p>
                <p>Thank you for your order! Your order <strong>{orderCode}</strong> has been successfully created and is now being processed. Below are the details of your order:</p>
                
                <h3>Order Information:</h3>
                <ul>
                    <li><strong>Order Status:</strong> {orderStatus}</li>
                    <li><strong>Total Price:</strong> {totalPrice}</li>
                    <li><strong>Delivery Address:</strong> {address}</li>
                    <li><strong>Recipient Name:</strong> {recipientName}</li>
                    <li><strong>Recipient Phone:</strong> {recipientPhone}</li>
                    <li><strong>Recipient Email:</strong> {recipientEmail}</li>
                </ul>
                
                <p>We are working hard to get your order to you as quickly as possible. You will receive another email once your order has been shipped. In the meantime, if you have any questions, feel free to reach out to us.</p>
                
                <p>Best Regards,</p>
                <p>Your Company Team</p>
                
                <div class='footer'>
                    <p>This email was sent to you because you made a purchase on our website.</p>
                </div>
            </div>
        </body>
        </html>";
    }

}
