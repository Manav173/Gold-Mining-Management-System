using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Services;
using Gold_Mining_Management_System.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostmarkDotNet;
using PostmarkDotNet.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Gold_Mining_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;

        public NotificationController(INotificationService notificationService, IUserRepository userRepository)
        {
            _notificationService = notificationService;
            _userRepository = userRepository;
        }

        // GET: api/notifications
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Notifications>>> GetNotifications()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        // GET: api/notifications/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Notifications>> GetNotification(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Notifications>> PostNotification(Notifications notification)
        {
            var sender = await _userRepository.GetUserById(notification.SendFrom);
            if (sender == null)
            {
                ModelState.AddModelError("SendFrom", "Invalid Sender ID.");
                return BadRequest(ModelState);
            }
            notification.Sender = sender;
            var receiver = await _userRepository.GetUserById(notification.SendTo);
            if (receiver == null)
            {
                ModelState.AddModelError("SendTo", "Invalid Receiver ID.");
                return BadRequest(ModelState);
            }
            notification.Receiver = receiver;
            var receiverEmail = notification.Receiver.Email;
            var message = new PostmarkMessage()
             {
                 To = receiverEmail,
                 From =notification.Sender.Email,
                 TrackOpens = true,
                 Subject = $"{notification.Type} Notification",
                 TextBody = notification.Message,
                 HtmlBody = $@"
                            <!DOCTYPE html>
                            <html lang='en'>
                            <head>
                                <meta charset='UTF-8'>
                                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                <title>{notification.Type} Notification</title>
                                <style>
                                    body {{
                                        font-family: Arial, sans-serif;
                                        line-height: 1.6;
                                        margin: 0;
                                        padding: 20px;
                                        background-color: #f4f4f4;
                                    }}
                                    .container {{
                                        max-width: 600px;
                                        margin: 0 auto;
                                        background-color: #ffffff;
                                        padding: 20px;
                                        border-radius: 5px;
                                        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                                    }}
                                    h1 {{
                                        color: #333;
                                        font-size: 24px;
                                        margin-bottom: 20px;
                                    }}
                                    h2 {{
                                        color: #333;
                                        font-size: 20px;
                                        margin-top: 0;
                                    }}
                                    p {{
                                        color: #555;
                                        font-size: 16px;
                                    }}
                                    .footer {{
                                        margin-top: 20px;
                                        text-align: center;
                                        font-size: 12px;
                                        color: #777;
                                    }}
                                    .sender {{
                                        margin-top: 10px;
                                        padding-top: 10px;
                                        border-top: 1px solid #ddd;
                                        text-align: left;
                                    }}
                                    .sender-info {{
                                        margin-top: 30px;
                                        padding-top: 20px;
                                        border-top: 1px solid #ddd;
                                        text-align: left;
                                    }}
                                </style>
                            </head>
                            <body>
                                <div class='container'>
                                    <h1>Gold Mining Management System</h1>
                                    <h2>Dear {notification.Receiver.Username},</h2>
                                    <p>{notification.Type} Notification</p>
                                    <p>{notification.Message}</p>
                                    <div class='sender'>
                                        <p>This message is sent to you by:-</p>
                                        <p>Sender: {notification.Sender.Username}</p>
                                        <p>Position: {notification.Sender.Role}</p>
                                        <p>Email me at {notification.Sender.Email} for any questions or if you require any further assistance.</p>
                                    </div>
                                    <div class='footer'>
                                        <p><strong>Timestamp:</strong> {notification.Timestamp.ToString("MMMM dd, yyyy HH:mm")}</p>
                                        <p>This is an automated message. Please do not reply directly to this email.</p>
                                    </div>
                                    <div class='sender-info'>
                                        <p><strong>Company:</strong> Gold Mining Management System</p>
                                        <p><strong>Contact Us:</strong> support@goldminingmanagement.com</p>
                                        <p><strong>Website:</strong> www.goldminingmanagement.com</p>
                                    </div>
                                </div>
                            </body>
                            </html>",
                Tag = "notification",
                //Headers = new HeaderCollection 
                //{
                //    { "X-CUSTOM-HEADER","Notification Header" } 
                //}
            };

            var client = new PostmarkClient("cf07c062-d114-49ec-a9bf-8e472a8949d4");
            var sendResult = await client.SendMessageAsync(message);
            await _notificationService.CreateNotificationAsync(notification);
            return CreatedAtAction(nameof(GetNotification), new { id = notification.NotificationId }, notification);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutNotification(int id, Notifications notification)
        {
            if (id != notification.NotificationId)
            {
                return BadRequest();
            }
            var sender = await _userRepository.GetUserById(notification.SendFrom);
            if (sender == null)
            {
                ModelState.AddModelError("SendFrom", "Invalid Sender ID.");
                return BadRequest(ModelState);
            }
            notification.Sender = sender;
            var receiver = await _userRepository.GetUserById(notification.SendTo);
            if (receiver == null)
            {
                ModelState.AddModelError("SendTo", "Invalid Receiver ID.");
                return BadRequest(ModelState);
            }
            notification.Receiver = receiver;

            await _notificationService.UpdateNotificationAsync(notification);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            await _notificationService.DeleteNotificationAsync(id);
            return NoContent();
        }
    }
}
