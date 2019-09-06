using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication2.Services
{
    public class AliceService
    {

        /// <summary>
        /// Коллекция для хранения ключевых слов и текущего состояния диалога. Здесь она заполняется значениями в конструкторе для примера. В случае надобности, перенести в отдельный метод для получения из значений базы данных
        /// </summary>
        private readonly List<AliceDialogItem> aliceDialogItems = new List<AliceDialogItem>();
        public AliceService()
        {
            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 1,
                ItemDescription = "help",
                SkillId = 3,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "помощь" } }
            });

            //что ты умеешь
            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 2,
                ItemDescription = "skils",
                SkillId = 3,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "что" }, new string[] { "ты" }, new string[] { "умеешь" } }
            });

            //вода

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 3,
                ItemDescription = "water",
                SkillId = 3,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "water" } }
            });

            //история заказов

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 4,
                ItemDescription = "ordersHistory",
                SkillId = 3,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "история", "историю" }, new string[] { "заказов", null }, }
            });

            //что в корзине

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 5,
                ItemDescription = "basket",
                SkillId = 3,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "корзину", "заказ", "корзина" }, new string[] { "расскажи", "открой", null } }
            });

            //меню

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 6,
                ItemDescription = "menu",
                SkillId = 3,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "расскажи", "открой", null }, new string[] { "меню" } }
            });

            ///подпункты меню
            
             aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 7,
                ItemDescription = "menuItems",
                SkillId = 3,
                ParentId = 6,
                Keywords = null 
            });
             
            

            //начало заказа

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 8,
                ItemDescription = "order",
                SkillId = 3,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "закажи", "заказать", "заказываю" } }
            });

            

            //конец заказа

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 9,
                ItemDescription = "orderEnd",
                SkillId = 3,
                ParentId = 10,
                Keywords = new List<string[]>() { new string[] { "заказ" }, new string[] { "завершить" } }
            });

            //доставка

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 10,
                ItemDescription = "delivery",
                SkillId = 3,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "доставку", "доставка" }, new string[] { "оформи", "организуй", null } }
            });

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 11,
                ItemDescription = "delivery",
                SkillId = 3,
                ParentId = 10,
                Keywords = new List<string[]>() { new string[] { "адрес", "телефон" }}
            });

            // clinic

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 1,
                ItemDescription = "help",
                SkillId = 4,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "помощь" } }
            });

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 2,
                ItemDescription = "skills",
                SkillId = 4,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "что" }, new string[] { "ты" }, new string[] { "умеешь" } }
            });

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 3,
                ItemDescription = "record",
                SkillId = 4,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] { "запиши", "записать", "запись" } }
            });

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 4,
                ItemDescription = "cabinets",
                SkillId = 4,
                ParentId = 0,
                Keywords = null
            });

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 5,
                ItemDescription = "dates",
                SkillId = 4,
                ParentId = 0,
                Keywords = null
            });

            aliceDialogItems.Add(new AliceDialogItem()
            {
                Id = 6,
                ItemDescription = "",
                SkillId = 4,
                ParentId = 0,
                Keywords = new List<string[]>() { new string[] {  } }
            });
        }
        /// <summary>
        /// Проверка полученного токена на валидность (?)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CheckAuthorization(string token, string session)
        {
            try
            {
                WebClient web = new WebClient();
                web.Headers.Add(HttpRequestHeader.Authorization, token);
                /*web.Proxy = new WebProxy()
                {
                    Address = new Uri("http://85.234.124.34:41088"),
                    BypassProxyOnLocal = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("telega", "client")
                };*/

                string reply = web.DownloadString("https://login.yandex.ru/info?format=json");
                YandexPassportReply result = JsonConvert.DeserializeObject<YandexPassportReply>(reply, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                var lunchService = new AliceLunchService();

                DataSet data = lunchService.Call("AliceUsersService", session, "GET", "USER", new string[] { result.ID });
                if (data.Tables[0].Rows.Count == 0)
                {
                    List<string> keysString = new List<string>();
                    List<string> valString = new List<string>();
                    var count = result.GetType().GetProperties();
                    foreach (var key in result.GetType().GetProperties())
                    {

                        if (key.GetValue(result) != null && key.GetValue(result).GetType() != typeof(string[]) && key.Name != "bIsAvatarEmpty")
                        {
                            keysString.Add(key.Name);
                            valString.Add("'" + key.GetValue(result).ToString() + "'");
                        }
                    }
                    string command = ("insert into AliceUsers("+String.Join(",", keysString)+")" +
                                            "values("+String.Join(",", valString)+");" +
                                            "insert into AliceSessions(UserID, SessionID)" +
                                            "values((select AliceUsers.AliceUser from AliceUsers where AliceUsers.ID = '"+result.ID+"'),'"+session+"'));");

                    lunchService.Execute(command);
                    return true;
                }
                else
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        if (row["ID"].ToString() == result.ID.ToString())
                        {
                            return true;
                        }
                    }
                    return false;
                }
            } catch (Exception e)
            {
                return false;
            }
        }



        /*public string ProcessClinicRequest(AliceRequest aliceRequest)
        {
            try
            {
                if (aliceRequest.Request.nlu.tokens.Count() == 0)
                {
                    return "helloString";
                }
                else
                {
                    var clinicService = new AliceClinicService();
                    FillCategoryKeywords(clinicService, aliceRequest.Session.SessionId, "AliceClinicService");
                    var result = MatchDialogState(aliceRequest.Request.nlu.tokens, 4);
                    switch (result.Id)
                    {
                        case 1:
                            {
                                return "helpString";
                            }
                        case 2:
                            {
                                return "whatdo";
                            }
                        case 3:
                            {
                                DataSet data = clinicService.Call("AliceClinicService", aliceRequest.Session.SessionId, "GET", "CABINET");
                                if (data.Tables[0].Rows.Count == 0)
                                    return "noFreeCabinets";
                                
                                
                                break;
                            }
                        case 4:
                            {
                                break;
                            }
                        case 5:
                            {
                                break;
                            }
                        case 6:
                            {
                                break;
                            }
                        case 7:
                            {
                                break;
                            }
                        case 8:
                            {
                                break;
                            }
                        case 9:
                            {
                                break;
                            }
                    }

                }
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }*/



        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string Decode(string input)
        {
            try
            {
                Encoding fromEncoding = System.Text.Encoding.GetEncoding("utf-8"); // может быть cp866 ? 
                Encoding toEncoding = System.Text.Encoding.GetEncoding("windows-1251"); // или utf-8
                byte[] toEncodeAsBytes = fromEncoding.GetBytes(input);
                string returnValue = fromEncoding.GetString(System.Text.Encoding.Convert(fromEncoding, toEncoding, toEncodeAsBytes));
                return returnValue;
            }
            catch
            {
                return "Ошибка декодирования";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="session"></param>
        /// <param name="procedure"></param>
        private void FillCategoryKeywords(object service, string session, string procedure)
        {
            DataSet data = new DataSet();
            string colName = "";
            int skillID = 0;
            int itemID = 0;
            switch (procedure)
            {
                case "AliceLunchService":
                    {
                        var lunchService = (AliceLunchService)service;
                        data = lunchService.Call(procedure, session, "GET", "MENU");
                        colName = "Category";
                        skillID = 3;
                        itemID = 7;
                        break;
                    }
                case "AliceClinicService":
                    {
                        var clinicService = (AliceClinicService)service;
                        data = clinicService.Call(procedure, session, "GET", "CABINET");
                        colName = "Cabinet";
                        skillID = 4;
                        itemID = 4;
                        break;
                    }
            }

            var maxCount = 0;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                maxCount = maxCount > row[colName].ToString().Split(' ').Count() ? maxCount : row[colName].ToString().Split(' ').Count();
            }

            List<string[]> keywords = new List<string[]>();
            for (var i = 0; i < maxCount; i++)
            {
                List<string> words = new List<string>();
                foreach (DataRow item in data.Tables[0].Rows)
                {
                    var items = Decode(item[colName].ToString()).Split(' ');
                    if (i < items.Count())
                    {
                        words.Add(items[i]);
                    }
                    else
                    {
                        words.Add(null);
                    }
                }
                keywords.Add(words.ToArray());
            }
            aliceDialogItems.Where(x => x.Id == itemID && x.SkillId == skillID).FirstOrDefault().Keywords = keywords;
        }
        
        
        
        /// <summary>
        /// Обработка запроса навыка "Обеды в офис"
        /// </summary>
        /// <param name="aliceRequest"></param>
        /// <returns></returns>
        public string ProcessLunchRequest(AliceRequest aliceRequest)
        {
            try
            {

                if (aliceRequest.Request.nlu.tokens.Count() == 0)
                {
                    return "Здравствуйте! ...";
                }
                else
                {
                    var lunchService = new AliceLunchService();
                    FillCategoryKeywords(lunchService, aliceRequest.Session.SessionId, "AliceLunchService");
                    var result = MatchDialogState(aliceRequest.Request.nlu.tokens, 3);
                    if (result == null) return "Не могу понять, чего вы хотите. Повторите команду.";

                    switch (result.Id)
                    {
                        case 1:
                            {
                                return "чтобы посмотреть меню произнесите \"Открой меню\", \r\n чтобы сделать заказ произдесите \"Заказать\" и Назовите блюдо из меню, \r\n" +
                                    "чтобы сделать доставку на адрес произнесите \"Оформи доставку\"";
                            }
                        case 2:
                            {
                                return "Навык \"\" позволяет посмотреть меню в ресторане \"Name\", сделать заказ в этом ресторане и оформить доставку по адресу";
                            }
                        case 3:
                            {
                                return "trash";
                            }
                        case 4:
                            {
                                //история заказов
                                DataSet data = lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "GET", "HISTORY", new string[] { aliceRequest.Session.UserId });
                                ///sessionId
                                ///orderId
                                ///date
                                ///
                                if (data.Tables[0].Rows.Count == 0)
                                {
                                    return "Еще не было сделано ни одного заказа";
                                }
                                string history = "Последние заказы: \r\n";
                                StringBuilder orders = new StringBuilder();
                                foreach (DataRow order in data.Tables[0].Rows)
                                {
                                    orders.Append(order.ItemArray[data.Tables[0].Columns.IndexOf("OrderDate")].ToString() + ": ");
                                    orders.Append("Заказ № " + order.ItemArray[data.Tables[0].Columns.IndexOf("ID")].ToString() + "\r\n");
                                }
                                return history + orders.ToString();
                            }
                        case 5:
                            {
                                //корзина
                                DataSet data = lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "GET", "ORDER");
                                ///sessionId
                                ///orderId
                                ///item
                                ///coast
                                if (data.Tables[0].Rows.Count == 0)
                                {
                                    return "Корзина пуста";
                                }

                                string basket = "Заказ №: " + data.Tables[0].Rows[0]["ID"].ToString() + "\r\n";
                                StringBuilder orders = new StringBuilder();
                                foreach (DataRow order in data.Tables[0].Rows)
                                {
                                    orders.Append(order["Item"].ToString() + ": ");
                                    orders.Append(order["Coast"].ToString() + "\r\n");
                                }
                                return basket + orders.ToString();
                            }
                        case 6:
                            {
                                //меню
                                DataSet data = lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "GET", "MENU");
                                ///groupItem
                                ///
                                if (data.Tables[0].Rows.Count == 0)
                                    return "Нет категорий в меню";

                                StringBuilder menu = new StringBuilder();
                                foreach (DataRow item in data.Tables[0].Rows)
                                {
                                    menu.Append(Decode(item["Category"].ToString()) + "\r\n");
                                }
                                return "Выберите категорию из нижеперечисленных: \r\n" + menu.ToString();
                            }
                        case 7:
                            {
                                ///элемент категории

                                DataSet data = lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "GET", "MENU", aliceRequest.Request.nlu.tokens);
                                if (data.Tables[0].Rows.Count == 0)
                                    return "Нет элементов категории";
                                StringBuilder menu = new StringBuilder();
                                foreach (DataRow item in data.Tables[0].Rows)
                                {
                                    menu.Append(item["Item"].ToString() + " : " + item["Coast"].ToString() + "\r\n");
                                }
                                return menu.ToString();
                            }
                        case 8:
                            {
                                //order
                                DataSet data = lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "GET", "ORDER");
                                if (data.Tables[0].Rows.Count == 0)
                                {
                                    lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "ADD", "ORDER");
                                }
                                else if (data.Tables[0].Rows[0]["OrderState"].ToString() == "ready")
                                {
                                    return "Заказ уже сформирован и отправлен, чтобы отменить заказ - позвоните 7(123) 456 - 78 - 90";
                                }
                                //addToOrder

                                //убираем из токена активационные фразы
                                //var itemWords = aliceRequest.Request.nlu.tokens.ToList().Where(x => result.Keywords[0].ToList().IndexOf(x) == -1);
                                var itemWords = aliceRequest.Request.nlu.tokens.ToList().Except(result.Keywords[0].ToList());

                                //запрос в базу на соответствие, возвращает точное название из меню
                                DataSet item = lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "UPD", "ORDER", itemWords.ToArray());
                                if (item.Tables[0].Rows[0]["MenuItem"] != null)
                                {
                                    return item.Tables[0].Rows[0]["MenuItem"] + ", добавлено к заказу, можете добавить к заказу чтото еще, либо завершить заказ";
                                }
                                else return "Кажется, такой позиции нет в меню этого ресторана, попробуйте еще раз";
                            }
                        case 9:
                            {
                                //order end
                                DataSet data = lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "END", "ORDER");
                                if (data.Tables[0].Rows[0]["OrderState"].ToString() == "ready")
                                {
                                    string address = "MagazineAddress";
                                    return $"Заказ № {data.Tables[0].Rows[0]["ID"]} принят, забрать его можно по адресу {address} или можете попросить меня оформить доставку";
                                }
                                else
                                {
                                    return "Произошла ошибка, попробуйте еще раз";
                                }
                            }
                        case 10:
                            {
                                //доставка
                                DataSet data = lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "GET", "DELIVERY");
                                if (data.Tables[0].Rows.Count == 0)
                                {
                                    lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "ADD", "DELIVERY");
                                    return "Скажите \"Адрес\" или \"Телефон\" и назовите адрес на который необходимо организовать доставку или телефон для подтверждения";
                                }
                                else
                                {
                                    if (data.Tables[0].Rows[0]["Address"].ToString() == "")
                                    {
                                        return "Необходимо записать адрес доставки, скажите \"Адрес\" и назовите адрес, на который необходимо организовать доставку";
                                    }
                                    else if (data.Tables[0].Rows[0]["Phone"].ToString() == "")
                                    {
                                        return "Необходимо записать номер телефона, скажите \"Телефон\" и назовите номер телефона";
                                    }
                                    else
                                    {
                                        return "Данные для доставки \r\n Адрес: " + data.Tables[0].Rows[0]["Address"] + ", Телефон: " + data.Tables[0].Rows[0]["Phone"];
                                    }
                                }
                                
                            }
                        case 11:
                            {
                                DataSet data = lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "GET", "DELIVERY");
                                var itemWords = aliceRequest.Request.nlu.tokens.ToList().Except(result.Keywords[0].ToList());
                                string phrase = String.Join(' ', itemWords);

                                if (data.Tables[0].Rows.Count == 0)
                                {
                                    return "Не понимаю о чем вы, попробуйте еще раз";
                                }
                                else
                                {
                                    if (aliceRequest.Request.nlu.tokens.Contains("адрес"))
                                    {
                                        if (data.Tables[0].Rows[0]["Address"].ToString() == "")
                                        {
                                            lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "ADD", "ADDRESS", new string[] { phrase });
                                            if (data.Tables[0].Rows[0]["Phone"].ToString() == "")
                                            {
                                                return "Адрес " + phrase + " записан, Необходимо записать номер телефона, скажите \"Телефон\" и назовите номер телефона";
                                            }
                                            else return "Адрес " + phrase + " записан";
                                        }
                                        else
                                        {
                                            lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "ADD", "ADDRESS", new string[] { phrase });
                                            if (data.Tables[0].Rows[0]["Phone"].ToString() == "")
                                            {
                                                return "Адрес " + phrase + " перезаписан, Необходимо записать номер телефона, скажите \"Телефон\" и назовите номер телефона";
                                            }
                                            else return "Адрес " + phrase + " перезаписан";
                                        }
                                    }
                                    else if (aliceRequest.Request.nlu.tokens.Contains("телефон"))
                                    {
                                        if (data.Tables[0].Rows[0]["Phone"].ToString() == "")
                                        {
                                            lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "ADD", "PHONE", new string[] { phrase });
                                            if (data.Tables[0].Rows[0]["Address"].ToString() == "")
                                            {
                                                return "Телефон " + phrase + " записан, Необходимо записать адрес доставки, скажите \"Адрес\" и назовите адрес, на который необходимо организовать доставку";
                                            }
                                            else return "Телефон " + phrase + " записан";

                                        }
                                        else
                                        {
                                            lunchService.Call("AliceLunchService", aliceRequest.Session.SessionId, "ADD", "PHONE", new string[] { phrase });
                                            if (data.Tables[0].Rows[0]["Address"].ToString() == "")
                                            {
                                                return "Телефон " + phrase + " перезаписан, Необходимо записать адрес доставки, скажите \"Адрес\" и назовите адрес, на который необходимо организовать доставку";
                                            }
                                            else return "Телефон " + phrase + " перезаписан";
                                        }
                                    }
                                    else return "Как вы сюда попали?";
                                }
                            }
                        default:
                            {
                                return "абсолютно неожиданная ошибка";
                            }
                    }
                }
            } catch (Exception ex)
            {
                return "К сожалению произошла ошибка. Попробуйте перезапустить навык. Прошу прощения за доставленные неудобства.";
            }
        }
        /// <summary>
        /// Ищем по ключевым словам совпадения токенов
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="skillId"></param>
        /// <returns></returns>
        public AliceDialogItem MatchDialogState(string[] tokens, int skillId)
        {
            var result = aliceDialogItems.Where(x=>x.SkillId == skillId).FirstOrDefault(x => CheckDialogItem(x.Keywords, tokens));
            return result;
        }
        /// <summary>
        /// Проаерка токенов на совпадения с ключевыми словами
        /// </summary>
        /// <param name="Keywords"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private bool CheckDialogItem(List<string[]> Keywords, string[] tokens)
        {
            bool result = true;
            if (Keywords != null)
            {
                foreach (var keyword in Keywords)
                {
                    if (keyword.Contains(null))
                    {
                        if (tokens.Intersect(keyword.Where(x => x != null)).Count() > 0)
                        {
                            result = result && (tokens.Intersect(keyword).Count() > 0);
                        }
                        else result = result || (tokens.Intersect(keyword).Count() > 0);
                    }
                    else
                    {
                        result = result && (tokens.Intersect(keyword).Count() > 0);
                    }
                }
                return result;
            }
            else return false;
            
        }
    }
}



//Обеды в офис
/*aliceDialogItems.Add(new AliceDialogItem
{
    Id = 1,
    ItemDescription = "help",
    SkillId = 3,
    ParentId = 0,
    Keywords = new List<string[]> { new string[] { "запоминай", "слушай" }, new string[] { "имя", "имена" } },
});

aliceDialogItems.Add(new AliceDialogItem
{
    Id = 2,
    ItemDescription = "whatDo",
    SkillId = 3,
    ParentId = 0,
    Keywords = new List<string[]> { new string[] { "запоминай", "слушай" }, new string[] { "имя", "имена" } },
});

aliceDialogItems.Add(new AliceDialogItem
{
    Id = 3,
    ItemDescription = "whereFrom",
    SkillId = 3,
    ParentId = 0,
    Keywords = new List<string[]> { new string[] { "запоминай", "слушай" }, new string[] { "имя", "имена" } },
});

aliceDialogItems.Add(new AliceDialogItem
{
    Id = 4,
    ItemDescription = "menuRoot",
    SkillId = 3,
    ParentId = 0,
    Keywords = new List<string[]> { new string[] { "запоминай", "слушай" }, new string[] { "имя", "имена" } },
});

aliceDialogItems.Add(new AliceDialogItem
{
    Id = 5,
    ItemDescription = "menuHot",
    SkillId = 3,
    ParentId = 4,
    Keywords = new List<string[]> { new string[] { "запоминай", "слушай" }, new string[] { "имя", "имена" } },
});

aliceDialogItems.Add(new AliceDialogItem
{
    Id = 6,
    ItemDescription = "menuCold",
    SkillId = 3,
    ParentId = 4,
    Keywords = new List<string[]> { new string[] { "запоминай", "слушай" }, new string[] { "имя", "имена" } },
});
aliceDialogItems.Add(new AliceDialogItem
{
    Id = 7,
    ItemDescription = "menuDrink",
    SkillId = 3,
    ParentId = 4,
    Keywords = new List<string[]> { new string[] { "запоминай", "слушай" }, new string[] { "имя", "имена" } },
});
// Я хочу сделать заказ
aliceDialogItems.Add(new AliceDialogItem
{
    Id = 8,
    ItemDescription = "purchaseStart",
    SkillId = 3,
    ParentId = 0,
    Keywords = new List<string[]> { new string[] { "запоминай", "слушай" }, new string[] { "имя", "имена" } },
});*/


/*
        //холодные блюда

        aliceDialogItems.Add(new AliceDialogItem()
        {
            Id = 7,
            ItemDescription = "menuCold",
            SkillId = 3,
            ParentId = 6,
            Keywords = new List<string[]>() { new string[] { "холодные" }, new string[] { "закуски" } }
        });

        //горячее

        aliceDialogItems.Add(new AliceDialogItem()
        {
            Id = 8,
            ItemDescription = "MenuHot",
            SkillId = 3,
            ParentId = 6,
            Keywords = new List<string[]>() { new string[] { "горячие" }, new string[] { "блюда" } }
        });

        //напитки

        aliceDialogItems.Add(new AliceDialogItem()
        {
            Id = 9,
            ItemDescription = "menuDrink",
            SkillId = 3,
            ParentId = 6,
            Keywords = new List<string[]>() { new string[] { "напитки" } }
        });*/


//выбор блюд для заказа

/*aliceDialogItems.Add(new AliceDialogItem()
{
    Id = 11,
    ItemDescription = "orderAdd",
    SkillId = 3,
    ParentId = 10,
    Keywords = new List<string[]>() { new string[] { "добавь", "еще" } }
});*/


/*case 8:
                        {
                            //hot
                            DataSet data = lunchService.Call("AliceLotService", aliceRequest.Session.SessionId, "GET", "HOT");
                            ///itemId
                            ///itemDesc
                            ///coast
                            StringBuilder menu = new StringBuilder();
                            foreach (DataRow item in data.Tables[0].Rows)
                            {
                                menu.Append(item.ItemArray[data.Tables[0].Columns.IndexOf("groupItem")].ToString() + "\r\n");
                            }
                            return menu.ToString();
                        }
                    case 9:
                        {
                            //drink
                            DataSet data = lunchService.Call("AliceLotService", aliceRequest.Session.SessionId, "GET", "DRINK");
                            ///itemId
                            ///itemDesc
                            ///coast
                            StringBuilder menu = new StringBuilder();
                            foreach (DataRow item in data.Tables[0].Rows)
                            {
                                menu.Append(item.ItemArray[data.Tables[0].Columns.IndexOf("groupItem")].ToString() + "\r\n");
                            }
                            return menu.ToString();
                        }*/
