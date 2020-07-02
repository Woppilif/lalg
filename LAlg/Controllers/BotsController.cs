using Microsoft.AspNetCore.Mvc;
using TelegramBot;
using VkBot;
namespace LAlg.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BotsController : ControllerBase
    {
        private TelegramClient _telegramClient;
        private BotVk _botVk;
        public BotsController(TelegramClient telegramClient, BotVk botVk)
        {
            _telegramClient = telegramClient;
            _botVk = botVk;
        }

        [HttpPost]
        public IActionResult Vk([FromBody]VkBot.Update update)
        {
            switch (update.Type)
            {
                // Ключ-подтверждение
                case "confirmation":
                    {
                        return Ok(_botVk.Code);
                    }

                // Новое сообщение
                case "message_new":
                    {
                        // Десериализация
                        _botVk.Recieve(update);
                        break;
                    }
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Telegram([FromBody]TelegramBot.Update update)
        {
            _telegramClient.Recieve(update);
            return Ok();
        }

        //[HttpGet]
        //public IActionResult WebHook()
        //{
        //    var url = Request;
        //    var hookUrl = $"{url.Scheme}://{url.Host}";
        //    _telegramClient.SetWebHook(hookUrl);
        //    return Ok("Webhook was set!");
        //}
    }
}