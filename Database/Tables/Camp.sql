﻿IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Camp]') AND type in (N'U'))
DROP TABLE [dbo].[Camp]

CREATE TABLE [dbo].[Camp](
	[Id] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Theme] [nvarchar](100) NOT NULL,
	[BeginDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[History] [nvarchar](max) NULL,
 CONSTRAINT [PK_Camp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET DATEFORMAT ymd;

SET IDENTITY_INSERT Camper ON

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (1
		   ,2004
           ,'Первый лагерь'
           ,'Мы старались'
           ,'2007-06-15'
           ,'2007-06-24'
           ,''
           ,'Господь послал необходимые средства для лагеря и после основательной
подготовки мы провели этот лагерь. В нем отдыхали 30 человек инвалидов.
Большого труда стоило убедить этих людей поехать в лагерь, потому что они
никогда не жили вне дома. Они волновались о том, будут ли условия проживания
в лагере подходящими для них. В лагере развеялись все их страхи и сомнения. Они
наслаждались пребыванием на природе, вкусной едой и общением друг с другом.
Трудно в это поверить, но многие из них не были на природе несколько лет, так
как некому было вытащить на улицу коляску, это очень тяжелая работа. Особенно
трогательным моментом было то, что один из инвалидов, когда его вынесли из
подъезда собственного дома, попросил дать посмотреть ему на свой двор – он не был
на нем в течении 15-ти последних лет.

В лагере некоторые из них впервые услышали о Боге и познакомились с
Евангелием. Когда учителя в группах предложили им принять Христа, несколько
человек покаялись и решили начать новую жизнь с Богом. Некоторые из инвалидов
говорят о том, что они еще не нашли свой путь к Богу, возможно в дальнейшем они
пойдут по этому пути. Мы все хотим быстрых результатов, но спасение совершается
различно, каждый человек приходит к Богу своим путем и в свое время. Два
человека, мать и сын покаялись уже в Харькове после возвращения из лагеря,
когда к ним домой зашел верующий брат, который руководил их группой в лагере.

После лагеря мы сделали встречу с инвалидами в нашей церкви. Также свозили
их микроавтобусами, имели духовное общение, затем чаепитие с бутербродами
и беседы друг с другом. В течение года мы поддерживали отношения, навещая
инвалидов дома. В одном из районов города была организована группа по изучению
Библии и 6 человек инвалидов, которые проживают недалеко друг от друга, смогли
ее посещать раз в неделю. Еще зимой инвалиды начали спрашивать о том, будет ли
лагерь в нынешнем году, потому что в их одинокой, тяжелой жизни этот лагерь стал
благословением, радостью и неоценимым летним отдыхом.')

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (2
		   ,2005
           ,'Второй лагерь'
           ,'Занки'
           ,'2005-06-15'
           ,'2005-06-24'
           ,''
           ,'В следующем году мы решили увеличить число инвалидов в лагере потому,
что друзья наших инвалидов, которые не решились поехать в лагерь в прошлом году,
теперь очень просились взять их в лагерь. Мы снова готовились к лагерю, изучали
уроки, которые будем преподавать инвалидам, готовили программу каждого дня,
а также молились о финансах, необходимых для проведения лагеря. Приближался
лагерь, но из необходимых денег у нас было чуть больше 1 / 4 всей суммы. Бог
допустил для нас испытание веры, для того, чтобы мы в полной мере поняли нашу
зависимость от Него. За две недели до лагеря Бог послал недостающую сумму денег,
это явилось для нас великим чудом.

В лагере отдыхали 50 инвалидов, 11 из них были инвалидами – колясочниками.
В этом году новеньким инвалидам было легче знакомиться с программой лагеря,
потому что те, кто уже был в лагере, чувствовали себя уверенно и все им объясняли.
Если в прошлом году только к концу лагеря инвалиды стали стройно петь на собраниях
и у костра, то в этом году с самого первого дня инвалиды с удовольствием пели
полюбившиеся им песни, и это пение звучало отлично. Самым любимым событием
дня был вечерний костер, у которого мы общались, пели, проводили конкурсы,
слушали свидетельства, сами инвалиды активно участвовали в общении. Они
чувствовали себя свободно в лагере и даже просили молиться о них. Через месяц
после лагеря мы также сделали встречу для них на свежем воздухе на территории
церкви. Они были рады необыкновенно этой встрече, возможности пообщаться со
старыми и новыми друзьями. В этом году мы чаще посещаем инвалидов, группа
молодежи каждую субботу навещает всех по очереди, также преподаватели лагеря
стараются не забывать и встречаться со своими учениками потому, что большинство
инвалидов не выходит из дома, и чувствует себя очень одиноко.

К празднику Рождества мы сделали для них продуктовые подарки, навестили
их дома и поздравили с праздником. Они были очень этому рады, так как любая
помощь для них очень важна. Также продолжаем изучать Библию в группе инвалидов:

с Нового года образовалась еще одна группа в другом районе города. Несколько
инвалидов регулярно посещают церковь.')

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (3
		   ,2006
           ,'Третий лагерь'
           ,'Занки'
           ,'2006-06-15'
           ,'2006-06-24'
           ,''
           ,'С Божьей помощью и молитвой мы провели 3-й год лагеря. В этот раз Господь
послал нам деньги заблаговременно. Но были другие испытания: серьезно болели
некоторые работники и координатор лагеря. Мы молились, чтобы Господь сделал
чудо – послал здоровье и силы для работы и Бог ответил на наши молитвы. Арендная
плата за лагерь возросла почти в два раза, продукты тоже подорожали, но Господь
восполнил наши нужды.

В лагере отдыхало 55 инвалидов, около 10 человек отказались поехать в самый
последний момент из-за плохого самочувствия. Господь благословил нас, и несколько
человек покаялось в лагере. Наш лагерь посетили работники Трансмирового радио и
рассказали о своей работе и о передачах, которые можно слушать в течение недели.
30 инвалидов изъявили желание иметь приемники, чтобы слушать христианские
передачи. Мы молились об этой нужде, и Бог расположил сердца членов разных
церквей нашего города пожертвовать средства на приобретение приемников. Мы
раздали инвалидам приемники в сентябре этого года.')

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (4
		   ,2007
           ,'Первый лагерь в Лимане'
           ,'Лиман'
           ,'2007-06-15'
           ,'2007-06-24'
           ,''
           ,'В этот раз число желающих поехать в лагерь увеличилось до 107 человек. Вместе
с работниками нас было 166 человек. Мы не были готовы к этому, и некоторым
пришлось отказать. Мы очень боялись оказаться в долговой яме и просили помощи у
Господа и Его мудрости во всех вопросах. Мы также были вынуждены обратиться ко
всем участникам лагеря с просьбой о добровольных пожертвованиях на лагерь. Люди
жертвовали и таким образом мы уменьшили долг за лагерь. К Сентябрю, по милости
Божией, мы погасили весь долг. В этом году мы нашли более удобное место для
лагеря, с минимальной платой за аренду. В других местах она возросла настолько, что
ее нереально было заплатить. Также, в этом году Бог послал нам в лагерь родителей
с детьми-инвалидами с синдромом Дауна. Наверное, это были самые благодарные
люди в нашем лагере. Некоторые из них не были на природе несколько лет, потому
что не так просто вывезти больного ребенка. Наши сестры были волонтерами у этих
детей, в то время, как родители были на Библейском уроке и у вечернего костра.
Сестры очень уставали, заботясь о таких детках, а родители общаются с ними каждый
день и никуда не могут уйти от своих проблем. Эти люди говорили, что им очень
спокойно и хорошо в лагере.')

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (5
		   ,2008
           ,'Дети Царя'
           ,'Принадлежность Богу'
           ,'2008-06-15'
           ,'2008-06-24'
           ,''
           ,'В этом году бензин подорожал в 1,5 раза, а еда в 1,8 раза в сравнении с прошлым годом.')

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (6
		   ,2009
           ,'Пришествие Иисуса Христа'
           ,'Признаки последнего времени и что будет с этим миром'
           ,'2009-06-15'
           ,'2009-06-24'
           ,''
           ,'Мы избрали тему «Пришествие Иисуса Христа», потому что, как мы думаем, эта
тема очень актуальна в наше время. Мы переживаем мировой финансовый кризис,
стихийные бедствия, новые неизлечимые болезни. Эти знаки показывают то, что мы
живем в последнее время. В этом году у нас было серьезное испытание в лагере:
мальчик инвалид упал с лестницы. Его доставили в больницу на скорой помощи. Все
мы были шокированы этим событием, и усердно молились Богу о здоровье этого
мальчика. Ему стало лучше, и через время он вернулся в лагерь. В этом году в лагере
присутствовало 95 инвалидов. В нем также впервые был слепые люди - их было 15
человек. Они были рады своему отдыху в лагере и возможности изучать Библию.
Один пастор продолжает работу с ними, навещая их раз в неделю, и разбирая с ними
Библии.')

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (7
		   ,2010
           ,'10 заповедей'
           ,'Изучение 10 заповедей'
           ,'2010-06-21'
           ,'2010-06-30'
           ,'Разбирали каждую заповедь подробно в свете Священного Писания'
           ,'')

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (8
		   ,2011
           ,'"Отче наш..."'
           ,'Смысл молитвы "Отче наш..."'
           ,'2011-06-21'
           ,'2011-06-30'
           ,'Изучали пошагово каждую фразу молитвы "Отче наш"'
           ,'')

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (9
		   ,2012
           ,'Притчи Иисуса Христа'
           ,'Притчи Иисуса Христа'
           ,'2012-06-19'
           ,'2012-06-28'
           ,'Описание лагеря'
           ,'Знаменательных событий небыло')

INSERT INTO [Camp].[dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (10
		   ,2013
           ,'Велик Господь наш!'
           ,'Раскрыть проявления Величия Бога во всех сферах бытия.'
           ,'2013-06-18'
           ,'2013-06-27'
           ,''
           ,'Идет подготовка к лагерю')

SET IDENTITY_INSERT Camper OFF

