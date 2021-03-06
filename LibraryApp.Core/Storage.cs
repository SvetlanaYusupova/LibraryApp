using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core
{
    class Storage
    {
        private const string filePathUsers = "../../Data/Users.json";
        private const string filePathAdmin = "../../Data/Admin.json";
        private const string filePathBooks = "../../Data/Books.json";
        private const string filePathNotifications = "../../Data/Notifications.json";

        public List<User> Users { get; private set; }
        public List<BookInLibrary> Books { get; private set; }
        public List<Admin> Admins { get; private set; }
        public List<Notification> Notifications { get; private set; }
       

        public Storage()
        {
            /*Users = new List<User> { };
            Books = new List<BookInLibrary>
            {
                // книги жанра "Фантастика"
                new BookInLibrary("Пикник на обочине", new List<string>() {"Борис Стругацкий", "Аркадий Стругацкий"}, "16+", "«Пикник на обочине» – одно из самых прославленных произведений братьев Стругацких, увлекательная история сталкеров – отчаянно смелых людей, на свой страх и риск снова и снова отправляющихся в место высадки пришельцев – аномальную Зону, полную опасностей и смертельных ловушек...", "Фантастика", 7, 7),
                new BookInLibrary("Американские боги", new List<string>() {"Нил Гейман"}, "18+", "«Американские боги» - одно из самых известных произведений Геймана. Это роман о богах, привезенных в Америку людьми из разных уголков мира, почитаемых, а потом забытых, и о том, к чему не может остаться равнодушным ни один мужчина: о поисках отца, родины, возлюбленной, о символической и реальной смерти.", "Фантастика", 3, 3),
                new BookInLibrary("Дюна", new List<string>() {"Фрэнк Герберт"}, "16+", "Роман «Дюна», первая книга прославленной саги, знакомит читателя с Арракисом – миром суровых пустынь, исполинских песчаных червей, отважных фрименов и таинственной специи. Безграничная фантазия автора создала яркую, почти осязаемую вселенную, в которой есть враждующие Великие Дома, могущественная Космическая Гильдия, загадочный Орден Бинэ Гессерит и неуловимые ассасины. По мотивам «Дюны» снял свой гениальный фильм Дэвид Линч, а в 2021 году вышла новая экранизация Дени Вильнёва, главные роли в которой исполнили Стеллан Скарсгард, Тимоти Шаламе, Зендая, Джейсон Момоа и другие.", "Фантастика", 14, 14),

                // книги жанра "Детектив"
                new BookInLibrary("Детективное агенство Дирка Джентли", new List<string>() {"Адамс Дуглас"}, "16+", "Там, где вам не помогут ни полиция, ни дипломированный психиатр, ни кембриджский профессор, вступает в действие детективное агентство Дирка Джентли – крупнейшего специалиста по бракоразводным процессам, пропавшим кошкам и пришельцам из иных миров. Кто, как не он, даст ответ на сложнейшие вопросы современности: Что общего у кошки и Кольриджа с квантовой механикой и кушеткой в стиле честерфилд? Может ли покойник подать в суд за клевету? И действительно ли в подсознании каждого из нас живет гений, готовый справиться с любой задачей – даже с той, что на первый взгляд кажется неразрешимой?", "Детектив", 8, 8),
                new BookInLibrary("Снеговик", new List<string>() {"Ю Несбё"}, "16+", "Ю Несбё — самый известный норвежский писатель, обладатель более двух десятков наград в номинациях «Книга года», «Лучший детектив года», «Выбор читателей», премии «Стеклянный ключ» за лучший скандинавский триллер. Его романы, переведенные более чем на 40 языков, отличает напряженная, безукоризненно выстроенная интрига, яркие характеры и всегда неожиданная развязка. Уже несколько лет в тот день, когда выпадает первый снег, в Норвегии бесследно исчезают замужние женщины. Невинная детская игра в снеговика оборачивается кошмаром, а для Харри Холе погоня за серийным убийцей становится охотой на тень. Преступник будто дразнит старшего инспектора, обретая все новые и новые обличья..", "Детектив", 15, 15),
                new BookInLibrary("Азазель", new List<string>() {"Борис Акунин"}, "16+", "«Азазель» – первый роман из серии о необыкновенном сыщике Эрасте Фандорине. Ему всего двадцать лет, но он удачлив, бесстрашен, благороден и привлекателен. Юный Эраст Петрович служит в полицейском управлении, по долгу службы и по велению сердца расследует крайне запутанное дело. Книги об Эрасте Фандорине насыщены информацией из истории Отечества и одновременно являются увлекательнейшим детективным чтением.", "Детектив", 5, 5),

                // книги жанра "Философия"
                new BookInLibrary("Система вещей", new List<string>() {"Жан Бодрийяр"}, "16+", "Жан Бодрийяр – французский философ-постмодернист и одна из крупнейших фигур гуманитарной науки второй половины XX века. Ему принадлежат такие концепции, как «гиперреальность», «симулякр» и «абсолютное событие», которые прочно закрепились в актуальной философии и теории современности. «Система вещей» – это одна из первых работ Жана Бодрийяра, в которой он разворачивает мощную критику общества потребления, намечая всю дальнейшую проблематику своей философии. Он выявляет характерные черты такого общества и акцентирует внимание на том влиянии, при котором пассивное изобилие превращается в активный элемент общественного сознания и человеческих взаимоотношений.", "Философия", 1, 1),
                new BookInLibrary("Искусство любить", new List<string>() {"Эрих Фромм"}, "16+", "Одна из самых известных работ Эриха Фромма — «Искусство любить» — посвящена непростым психологическим аспектам возникновения и сохранения человеком такого, казалось бы, простого чувства, как любовь. Действительно ли любовь — искусство? Если да, то она требует труда и знаний. Или это только приятное ощущение?.. Для большинства проблема любви — это прежде всего проблема того, как быть любимым, а не того, как любить самому...", "Философия", 3, 3),
                new BookInLibrary("О вещах действительно важных", new List<string>() {"Питер Сингер"}, "16+", "Питера Сингера часто называют самым влиятельным философом в мире. И он же является одним из самых противоречивых. Будучи автором таких важных книг, как «Освобождение животных» и «Практическая этика», он защищает права животных, поддерживает альтруистические движения и способствует развитию биоэтики. В своей новой книге «О вещах действительно важных» Питер Сингер демонстрирует умение анализировать важнейшие события современности в формате небольших эссе. Сингер задается вопросом, можно ли считать шимпанзе людьми, нужно ли запрещать курение на законодательном уровне, а также ставит под вопрос святость человеческой жизни, подкрепляя свои утверждения последними мировыми событиями. В дополнение к этому он в простой и доступной форме освещает философские вопросы, такие как действительно ли в мире есть что-то важное и имеет ли какой-то смысл существование маленького голубого шарика под названием планета Земля. Провокационные и оригинальные, эти эссе заставят вас задуматься над своими убеждениями и, возможно, пересмотреть отношение к этическим проблемам, с которыми люди сталкиваются каждый день.", "Философия", 2, 2),

                // книги жанра "Роман"
                new BookInLibrary("Преступление и наказание", new List<string>() {"Фёдор Достоевский"}, "16+", "«Преступление и наказание» - высочайший образец криминального романа. В рамках жанра полицейского расследования писатель поставил вопросы, и по сей день не решенные. Кем должен быть человек: «тварью дрожащей», как говорит Раскольников, или «Наполеоном»? И это проблема уже XXI века. Написанный в 1866 году роман «Преступление и наказание» до сих пор остается самой читаемой в мире книгой и входит в большинство школьных программ по литературе.", "Роман", 5, 5),
                new BookInLibrary("Эмма", new List<string>() {"Джейн Остин"}, "16+", "Творчество знаменитой писательницы Джейн Остин, автора таких изящных и искрометных романов, как «Гордость и предубеждение», «Мэнсфилд-парк», «Эмма», «Нортенгерское аббатство», оказавшее ощутимое влияние на развитие английской прозы XIX и XX веков, получило широчайшее признание во всем мире. Романы Остин выдерживают все новые и новые издания, появляются все новые и новые их экранизации. В настоящем издании представлено самое едкое, самое точное и саркастичное из произведений писательницы — роман «Эмма». Больше всего Эмма, умница, красавица, дочь состоятельного помещика, любит помогать людям в поисках подходящих спутников жизни. Но почему-то все ее протеже действуют совсем не по ее планам... Даже она сама!", "Роман", 2, 2),
                new BookInLibrary("Мартин Иден", new List<string>() {"Джек Лондон"}, "16+", "«Мартин Иден» — самый известный роман Джека Лондона, впервые напечатанный в 1908-1909 гг. Во многом автобиографическая книга о человеке, который «сделал себя сам», выбравшись из самых низов, добился признания. Любовь к девушке из высшего общества побуждает героя заняться самообразованием. Он становится писателем, но все издательства отказывают ему в публикации. И как это часто бывает в жизни, пройдя сквозь лишения и унижения, получив отказ от любимой девушки, он наконец становится знаменитым. Но ни слава, ни деньги, ни успех, ни даже возвращение его возлюбленной не могут уберечь Мартина от разочарования в этой насквозь фальшивой жизни.", "Роман", 1, 1)

            };
            Admins = new List<Admin> { new Admin("Admin", "qwerty") };
            Notifications = new List<Notification> { };*/
            ReadUsers();
            ReadAdmin();
            ReadBooks();
            ReadNotifications();

            //стереть бронирование с датой, когда срок взятия вышел
            //заполнить уведомления для пользователей
            foreach (var user in Users)
            {
                user.DeleteMessages();
                foreach (var booking in user.GetOrderBook())
                {
                    if (booking.GetEndDate() < DateTime.Today)
                    {
                        user.DicreaseOrderBook(booking);
                        foreach (var book in Books)
                        {
                            if (book.GetBookName() == booking.GetBookName())
                            {
                                book.AddOneBook();
                                break;
                            }
                        }
                        foreach (var item in Notifications)
                        {
                            if (item.GetLogin() == user.GetLogin() && item.GetBookName() == booking.GetBookName() && item.GetType() == "booking")
                            {
                                Notifications.Remove(item);
                            }
                        }
                    }
                    else if ((DateTime.Today - booking.GetEndDate()).Duration().Days < 3)
                    {
                        user.AddBookingMessage(booking);
                    }
                }

                foreach (var taken in user.GetTakenBooks())
                {
                    if ((DateTime.Today - taken.GetEndDate()).Duration().Days < 7)
                    {
                        user.AddTakenMessage(taken);
                    }
                }
            }
            SaveUsers();
        }

        public void SaveNotifications()
        {
            using (var sw = new StreamWriter(filePathNotifications))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    serializer.Serialize(jsonWriter, Notifications);
                }
            }
        }

        private void ReadNotifications()
        {
            using (var sr = new StreamReader(filePathNotifications))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    Notifications = serializer.Deserialize<List<Notification>>(jsonReader);
                }
            }
        }

        public void SaveUsers()
        {
            using (var sw = new StreamWriter(filePathUsers))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    serializer.Serialize(jsonWriter, Users);
                }
            }
        }

        private void ReadUsers()
        {
            using (var sr = new StreamReader(filePathUsers))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    Users = serializer.Deserialize<List<User>>(jsonReader);
                }
            }
        }

        public void SaveBooks()
        {
            using (var sw = new StreamWriter(filePathBooks))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    serializer.Serialize(jsonWriter, Books);
                }
            }
        }

        private void ReadBooks()
        {
            using (var sr = new StreamReader(filePathBooks))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    Books = serializer.Deserialize<List<BookInLibrary>>(jsonReader);
                }
            }
        }

        public void SaveAdmin()
        {
            using (var sw = new StreamWriter(filePathAdmin))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    serializer.Serialize(jsonWriter, Admins);
                }
            }
        }

        private void ReadAdmin()
        {
            using (var sr = new StreamReader(filePathAdmin))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    Admins = serializer.Deserialize<List<Admin>>(jsonReader);
                }
            }
        }
    }
}
