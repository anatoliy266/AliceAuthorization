using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication2.Models
{
    public class YandexPassportReply
    {
        /// <summary>
        /// логин пользователя
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// ---
        /// </summary>
        [JsonProperty("old_social_login")]
        public string OldLogin { get; set; }

        /// <summary>
        /// емейл пользователя
        /// </summary>
        [JsonProperty("default_email")]
        public string Email { get; set; }

        /// <summary>
        /// имя пользователя
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// отображаемое имя
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Дата рождения пользователя
        /// </summary>
        [JsonProperty("birthday")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// пол пользователя
        /// </summary>
        [JsonProperty("sex")]
        public string Sex { get; set; }

        /// <summary>
        /// признак наличия аватара
        /// </summary>
        [JsonProperty("is_avatar_empty")]
        public string bIsAvatarEmpty { get; set; }

        /// <summary>
        /// id дефолтного аватара
        /// </summary>
        [JsonProperty("default_avatar_id")]
        public string DefaultAvatarId { get; set; }

        /// <summary>
        /// уникальный идентификатор пользователя
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>
        /// идентификатор приложения с которого был отправлен запрос
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientID { get; set; }

        /// <summary>
        /// список емейл адресов пользователя
        /// </summary>
        [JsonProperty("emails")]
        public string[] AccountEmails { get; set; }

        /// <summary>
        /// Список всех OpenID-идентификаторов, которые пользователь мог получить от Яндекса
        /// </summary>
        [JsonProperty("openid_identities")]
        public string[] OpenIDIdentities { get; set; }
    }
}

