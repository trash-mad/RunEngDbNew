using System;
using System.Collections.Generic;
using System.Text;

namespace DbElems
{
    public class DefaultCategory
    {
        private DefaultCategory(string Category,string Subcategory)
        {
            this.Category = Category;
            this.Subcategory = Subcategory;
        }

        public string Category { get; private set; }
        public string Subcategory { get; private set; }


        public static DefaultCategory[] GetDefaultCategories()
        {
            List<DefaultCategory> tmp = new List<DefaultCategory>()
            {
                new DefaultCategory("Привод", "Электродвигатель"),
                new DefaultCategory("Привод", "Двигатель внутреннего сгорания"),
                new DefaultCategory("Привод", "Гидромотор"),
                new DefaultCategory("Привод", "Пневмопривод"),

                new DefaultCategory("Насос", "Центробежный"),
                new DefaultCategory("Насос", "Вакуумный водокольцевой"),
                new DefaultCategory("Насос", "Поршневой"),
                new DefaultCategory("Насос", "Плунжерный"),
                new DefaultCategory("Насос", "Шнековый"),
                new DefaultCategory("Насос", "Перистальтический"),
                new DefaultCategory("Насос", "Шестеренный"),
                new DefaultCategory("Насос", "Аксиально-поршневой"),

                new DefaultCategory("Генератор", "Синхронный"),
                new DefaultCategory("Генератор", "Асинхронный"),


                new DefaultCategory("Клапан", "Предохранительный"),
                new DefaultCategory("Клапан", "Регулировочный"),
                new DefaultCategory("Клапан", "Игольчатый (дроссельный)"),

                new DefaultCategory("Гидравлический распределитель", "Многоблочный"),
                new DefaultCategory("Гидравлический распределитель", "Многосекционный"),

                new DefaultCategory("Шаровой кран ВД", "Двухходовый"),
                new DefaultCategory("Шаровой кран ВД", "Трехходовый"),

                new DefaultCategory("Шланговый барабан", "Ручной"),
                new DefaultCategory("Шланговый барабан", "С электроприводом"),
                new DefaultCategory("Шланговый барабан", "С гидроприводом"),
                new DefaultCategory("Шланговый барабан", "С автонамотчиком"),

                new DefaultCategory("Форсунок", "Очистной"),
                new DefaultCategory("Форсунок", "Каналопромывочный"),
                new DefaultCategory("Форсунок", "Гидрообразивный"),
                new DefaultCategory("Форсунок", "Увлажение и туманообразнование"),
                new DefaultCategory("Форсунок", "Пожаротушение"),

                new DefaultCategory("Частотный преобразователь", "Непосредственный"),
                new DefaultCategory("Частотный преобразователь", "Двухзвенный"),

                new DefaultCategory("Компрессор", "Винтовой (ротационный)"),
                new DefaultCategory("Компрессор", "Поршневой")
            };
            return tmp.ToArray();
        }
    }
}
