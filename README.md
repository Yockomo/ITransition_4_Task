# ITransition_4_Task
Web-приложение, которое позволяет пользователям зарегистрироваться и аутентифицироваться. Неаутентифицированные пользователи не имеют доступа к управлению пользователями (только к форме регистрации или форме аутентификации).
Аутентифицированные пользователи видят таблицу "пользователи" (идентификатор, именем, почтой, датой регистрации, датой последнего логина, статусом) с пользователями.
Таблица левой колонкой содержит чек-боксы для множественного выделения, в заголовке колонки чек-бокс "выделить все/снять выделение". Над таблицей тулбар с действиями: Block, Unblock, Delete.
Пользователь может удалить или заблокировать себя — при этом сразу должен быть разлогинен. Если кто-то другой блокирует или удаляет пользователя, то при любом следующем действии пользователь переправляется на страницу логина.
При регистрации есть возможность использовать любой пароль, даже из одного символа. Заблокированный пользователь не может войти, удаленный может заново зарегистрироваться.
