using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    /// <summary>
    /// Модель фазы диалога с Алисой (или с кем-нибудь другим)
    /// </summary>
    public class AliceDialogItem
    {
        /// <summary>
        /// Идентификатор фазы диалога
        /// </summary>
        public int Id { get; set; }
        public int ParentId { get; set; } = 0;

        public int SkillId { get; set; }
        /// <summary>
        /// Коллекция массивов строк: в массиве операция ИЛИ в коллекции операция И. Например ("приготовь" ИЛИ "свари") И ("кофе" ИЛИ "кофейку")
        /// </summary>
        public List<string[]> Keywords { get; set; }
        /// <summary>
        /// Описание фазы диалога
        /// </summary>
        public string ItemDescription { get; set; }

    }
}
