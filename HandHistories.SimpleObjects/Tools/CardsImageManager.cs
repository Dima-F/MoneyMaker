using System;
using System.Drawing;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleObjects.Tools
{
    /// <summary>
    /// Ф:Класс, который работает из встроенными ресурсами изображений (а именно изображения игральных карт).
    /// Отметим, что для того, чтобы поместить изображение в ресурс при построении проэкта, нужно выбрать в свойствах 
    /// изображения - действие при построении - внедренный ресурс.
    /// </summary>
    public static  class CardsImageManager
    {
        public static  Image GetImageCard(Card card)
        {
            var key = string.Format(@"HandHistories.SimpleObjects.Cards_images.{0}.png",
                ((byte)card).ConvertByteCardToString());
            return ExtractFromResource(key);
        }

        public static Image GetShirt()
        {
            var key = string.Format(@"HandHistories.SimpleObjects.Cards_images.{0}.png","Shirt");
            return ExtractFromResource(key);
        }

        private static Image ExtractFromResource(string key)
        {
            System.Reflection.Assembly execAssem =
                System.Reflection.Assembly.GetExecutingAssembly();
            var res = execAssem.GetManifestResourceStream(key);
            if (res != null) return new Bitmap(res);
            throw new NullReferenceException(string.Format("Can't find resource named : {0}", key));
        }
    }
}
