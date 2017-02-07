---
author: joannaleecy
title: Using cloud services for UWP games
description: Learn more about implementing cloud as a backend for your UWP games.
ms.assetid: 1a7088e0-0d7b-11e6-8e05-0002a5d5c51b
ms.author: joanlee
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, cloud services
---
#  Using cloud services for UWP games

The Universal Windows Platform (UWP) in Windows 10 offers a set of APIs that can be used for developing games across Microsoft devices. When developing games across platforms and devices, you can make use of a cloud backend to help scale your game according to demand.

##  What is cloud computing?

Cloud computing uses on demand IT resources and applications over the internet to store and process data for your devices. The term _cloud_ is a metaphor for the availability of vast resources out there (not local resources) that you can access from non-specific locations.
The principle of cloud computing offers a new way in which resources and software can be consumed. Users no longer need to pay for the full complete product or resources upfront, but instead are able to consume platform, software, and resources as a service. Cloud providers often bill their customers according to usage or service plan offerings.

##  Why use cloud services?

One advantage of using cloud services for games is that you do not need to invest in physical hardware servers upfront, but only need to pay according to usage or service plans at a later stage. It is one way to help manage the risks involved in developing a new game title. 

Another advantage is that your game can tap into vast cloud resources to achieve scalability (effectively manage any sudden spikes in the number of concurrent players, intense real-time game calculations or data requirements). This keeps the performance of your game stable around the clock. Furthermore, cloud resources can be accessed from any device running on any platform anywhere in the world, which means that you are able to bring your game to everyone globally.

Delivering an amazing gameplay experience to your players is important. Because game servers running in the cloud are independent of client-side updates, they can give you a more controlled and secure environment for your game overall.   You can also achieve gameplay consistency through the cloud by never trusting the client and having server side game logic. Service-to-service connections can also be configured to allow a more integrated gaming experience; examples include linking in-game purchases to various payment methods, bridging over different gaming networks, and sharing in-game updates to popular social media portals such as Facebook and Twitter. 

You can also use dedicated cloud servers to create a large persistent game world, build up a gamer community, collect and analyze gamer data over time to improve gameplay, and optimize your game's monetization design model.

In addition, games that require intensive game data management capabilities like social games with asynchronous multiplayer mechanics can be implemented using cloud services.

##  How game companies use the cloud technology

Learn how other developers have implemented cloud solutions in their games.

<table>
    <colgroup>
    <col width="10%" />
    <col width="30%" />
    <col width="30%" />
    <col width="30%" />
    </colgroup>
    <tr class="header">
        <th>Developer</th>
        <th>Description</th>
        <th>Key game scenarios</th>
        <th>Learn more</th>
    </tr>
    <tr>
        <td>[343 Industries](https://www.halowaypoint.com/)</td>
        <td>_Halo 5: Guardians_ implemented [Halo: Spartan Companies](https://www.halowaypoint.com/spartan-companies) as its social gameplay platform by using Microsoft Azure DocumentDB, which was selected for its speed and flexibility due to its auto-indexing capabilities.</td>
        <td>
            <ul>
                <li>Scalable data-tier to handle groups creation/management for multiplayer gameplay
                <li>Game and social media integration
                <li>Real-time queries of data through multiple attributes
                <li>Synchronization of gameplay achievements and stats
            </ul>
        </td>
        <td>
            <ul>
                <li>[Social gameplay implemented using Azure DocumentDB](https://azure.microsoft.com/blog/how-halo-5-guardians-implemented-social-gameplay-using-azure-documentdb/)</td>
            </ul>
    </tr>
    <tr>
        <td>[Illyriad Games](http://web.ageofascent.com/)</td>
        <td>Illyriad Games created _Age of Ascent_, a massively multiplayer online (MMO) epic 3D space game that can be played on devices that have modern browsers. So this game can be played on PCs, laptops, mobile phones and other mobile devices without plug-ins. The game uses ASP.NET Core, HTML5, WebGL, and Microsoft Azure.</td>
        <td>
            <ul>
                <li>Cross-platform, browser-based game
                <li>Single large persistent open world
                <li>Handles intensive real-time gameplay calculations
                <li>Scales with number of players
            </ul>
        </td>
        <td>
            <ul>
                <li>[Manage game components as microservices using Azure Service Fabric (video)](https://channel9.msdn.com/Events/Build/2016/KEY02#time=57m20s)  
                <li>[Interview with Age of Ascent developers (video)](https://channel9.msdn.com/Shows/Azure-Friday/Age-of-Ascent-from-Illyriad-Powered-by-Azure-Service-Fabric-and-ASPNET)
            </ul>
        </td>
    </tr>
    <tr>
        <td>[Next Games](http://www.nextgames.com/)</td>
        <td>Next Games is the creator of _The Walking Dead: No Man's Land_ video game which is based on AMC's original series. The Walking Dead game used Azure as the backend. It had 1,000,000 downloads in the opening weekend and within the first week, the game became #1 iPhone & iPad Free App in the U.S. App Store, #1 Free App in 12 countries, and #1 Free Game in 13 countries.
        </td>
        <td>
            <ul>
                <li>Cross-platform
                <li>Turn based multiplayer
                <li>Elastically scale performance
            </ul>
        </td>
        <td>
            <ul>
                <li>[Interview with Kalle Hiitola, CTO of Next Games (video)](https://channel9.msdn.com/Blogs/AzureDocumentDB/azure-documentdb-walking-dead)
                <li>[Walking Dead uses DocumentDB for faster development cycle and more engaging gameplay](https://azure.microsoft.com/blog/the-walking-dead-no-mans-land-game-soars-to-1-with-azure-documentdb/)
            </ul>
    </tr>
    </td>
        <td>[Pixel Squad](http://www.crimecoast.com/)</td>
        <td>Pixel Squad developed _Crime Coast_ using Unity game engine and Azure. _Crime Coast_ is a social strategy game available on the Android, iOS and Windows platform. Azure Blob storage, Managed Azure Redis Cache, an array of load balanced IIS VMs, and Microsoft Notification hub were used in their game. Learn how they managed scaling and handled players surge with 5000 simultaneous players.
        </td>
        <td>
            <ul>
                <li>Cross-platform
                <li>Multiplayer online game
                <li>Scale with number of players
            </ul>
        </td>
        <td>
            <ul>
                <li>[How Crime Coast MMO game used Azure Cloud Services](https://channel9.msdn.com/Blogs/The-Game-Blog/BizSpark-Interview-with-Pixel-Squad-How-the-used-Azure-Cloud-Services-to-make-an-MMO-with-a-3-man-te)
            </ul>
        </td>
    </tr> 
</table>

    
### Other links

* [Azure as the secret sauce for Hitcents, Game Troopers and InnoSpark](http://news.microsoft.com/features/game-developers-use-microsoft-azure-as-secret-sauce-for-scale-and-growth-2/)
* [Game startups on Bizspark program using Azure](https://blogs.technet.microsoft.com/bizspark_featured_startups/2015/09/25/azure-open-for-gaming-startups/)


## How to design your cloud backend

While producers and game designers are in discussion about what game features and functionalities are needed in the game, it is good to start considering how you want to design your game infrastructure. Azure can be used as your game backend when you want to develop games for various devices and across different major platforms.

### Step by step learning guides

* [Build 2016 Codelabs: Use Microsoft Azure App Service and Microsoft SQL Azure backend to save game score](https://github.com/Microsoft-Build-2016/CodeLabs-GameDev-6-Azure)
* [Design your game's mobile engagement strategy](https://azure.microsoft.com/documentation/articles/mobile-engagement-gaming-scenario/)
* [Using Azure Mobile Engagement for Unity iOS deployment](https://azure.microsoft.com/documentation/articles/mobile-engagement-unity-ios-get-started/)

### Understanding IaaS, PaaS or SaaS

First, you need to think about the level of service that is best suited for your game. Knowing the differences in the following three services can help you determine the approach you want to take in building your backend.

* [Infrastructure as a Service (IaaS)](https://azure.microsoft.com/overview/what-is-iaas/)

    Infrastructure as a Service (IaaS) is an instant computing infrastructure, provisioned and managed over the Internet. Imagine having the possibility of many machines readily available to quickly scale up and down depending on demand. IaaS helps you to avoid the cost and complexity of buying and managing your own physical servers and other datacenter infrastructure.

* [Platform as a Service (PaaS)](https://azure.microsoft.com/overview/what-is-paas/)

    Platform as a Service (PaaS) is like IaaS but it also includes management of infrastructure like servers, storage, and networking. So on the top of not buying physical servers and datacenter infrastructure, you also do not need to buy and manage software licenses, underlying application infrastructure, middleware, development tools, or other resources.

* Software as a Service (SaaS)

    Software as a Service is normally an application already built for you and hosted on an existing cloud platform. It is designed to make it even easier for you to start running your game on their service.


### Design your game infrastructure using Azure

Following are some ways that Azure cloud offerings can be used for a game. Azure works with Windows, Linux, and familiar open source technologies such as Ruby, Python, Java, and PHP. For more information, see [Azure for gaming](https://azure.microsoft.com/solutions/gaming/).

| Requirements                 | Activity scenarios                            | Product Offering                      | Product Capabilities                               |
|-----------------------------------|-----------------------------------------------|---------------------------------------|----------------------------------------------------|
| Host your domain in the cloud     | Respond to DNS queries efficiently            | [Azure DNS](https://azure.microsoft.com/services/dns/) | Host your domain with high performance and availability	|
| Sign in, identity verification      | Gamer signs in and gamer identity is authenticated  | [Azure Active Directory](https://azure.microsoft.com/services/active-directory/) | Single sign-on to any cloud and on-premises web app with multi-factor authentication            |
| Game using infrastructure as a service model (IaaS)      | Game is hosted on virtual machines in the cloud       | [Azure VMs](https://azure.microsoft.com/services/virtual-machines/) | Scale from 1 to thousands of virtual machine instances as game servers with built-in virtual networking and load balancing; hybrid consistency with on-premises systems           |
| Web or mobile games using platform as a service model (PaaS)            | Game is hosted on a managed platform	            | [Azure App Service](https://azure.microsoft.com/services/app-service/) | PaaS for websites or mobile games (which means Azure VMs with middleware/development tools/BI/DB management)   |
| Cloud storage for game data       | Latest game data is stored in the cloud and sent to client devices | [Azure Blob Storage](https://azure.microsoft.com/services/storage/blobs/)| No restriction on the kinds of file that can be stored; object storage for large amounts of unstructured data like images, audio, video, and more.  |
| Temporary data storage tables| Game transactions (changes in game states) are stored in tables temporarily | [Azure Table Storage](https://azure.microsoft.com/services/storage/tables/)| Game data can be stored in a flexible schema according to the needs of the game |
| Queue game transactions/requests| Game transactions are processed in the form of a queue | [Azure Queue Storage](https://azure.microsoft.com/services/storage/queues/)| Queues absorb unexpected traffic bursts and can prevent servers from being overwhelmed by a sudden flood of requests during the game   |
| Scalable relational game database| Structured storage of relational data like in-game transactions to database | [Azure SQL Database](https://azure.microsoft.com/services/sql-database/)| SQL database as a service ([Compare with SQL on a VM](https://azure.microsoft.com/documentation/articles/data-management-azure-sql-database-and-sql-server-iaas/))  |
| Scalable distributed low-latency game database| Fast read, write, and query of game and player data with schema flexibility | [Azure DocumentDB](https://azure.microsoft.com/services/documentdb/)| Low latency NoSQL document database as a service   |
| Use own datacenter with Azure services | Game is retrieved from your own datacenter and sent to the client devices | [Azure Stack](https://azure.microsoft.com/overview/azure-stack/) | Enables your organization to deliver Azure services from your own datacenter to help you achieve more  |
| Large data chunks transfer| Large files such as game images, audio, and videos can be sent to users from the nearest Content Delivery Network (CDN) pop location with Azure CDN	 | [Azure Content Delivery Network](https://azure.microsoft.com/services/cdn/) | Built on a modern network topology of large centralized nodes, Azure CDN handles sudden traffic spikes and heavy loads to dramatically increase speed and availability, resulting in significant user experience improvements  |
| Low latency               | Perform caching to build fast, scalable games with more control and guaranteed isolation of data; can be used to improve match-making feature for game as well. | [Azure Redis Cache](https://azure.microsoft.com/services/cache/) | High throughput, consistent low-latency data access to power fast, scalable Azure applications  |
| High scalability, low latency | Handles fluctuations in the number of game users with low latency read and writes | [Azure Service Fabric](https://azure.microsoft.com/services/service-fabric/) | Able to power the most complex, low-latency, data-intensive scenarios and reliably scale to handle more users at a time. Service Fabric enables you to build games without having to create a separate store or cache, as required for stateless apps |
| Ability to collect millions of events per second from devices                         | Log millions of events per second from devices | [Azure Event Hubs](https://azure.microsoft.com/services/event-hubs/) | Cloud-scale telemetry ingestion from games, websites, apps, and devices  |
| Real time processing for game data  | Perform real-time analysis of gamer data to improve gameplay| [Azure Stream Analytics](https://azure.microsoft.com/services/stream-analytics/) | Real-time stream processing in the cloud  |
| Develop predictive gameplay	      | Create customized dynamic gameplay based on gamer data	| [Azure Machine Learning](https://azure.microsoft.com/services/machine-learning/) | A fully managed cloud service that enables you to easily build, deploy, and share predictive analytics solutions  |
| Collect and analyze game data| Massive parallel processing of data from both relational and non-relational databases | [Azure Data Warehouse](https://azure.microsoft.com/services/sql-data-warehouse/)| Elastic data warehouse as a service with Enterprise class features   |
| Create marketing campaigns to increase usage and retention  |	Send push notifications to targeted players to generate interest and encourage specific game actions according to data analysis | [Mobile engagement](https://azure.microsoft.com/services/mobile-engagement/) |  Increase gameplay time and user retention on all major platforms—iOS, Android, Windows, Windows Phone |


##  Startup and developer resources

* [Microsoft BizSpark](https://www.microsoft.com/bizspark/)

    Microsoft BizSpark is a global program that helps startups succeed by giving free access to Azure cloud services, software and support. BizSpark members receive five Visual Studio Enterprise with MSDN subscriptions, each with a $150 monthly Azure credit. This totals $750/month across all five developers to spend on Azure services. BizSpark is available to startups that are privately held, less than 5-years-old, and earn less than $1M in annual revenue. Microsoft believes that by helping startups succeed, we’re helping to build a valued long-term partnership.
    
* [ID@Xbox](http://www.xbox.com/Developers/id)

    If you want to add Xbox Live features like multiplayer gameplay, cross-platform matchmaking, Gamerscore, achievements, and leaderboards to your Windows 10 game, sign up with ID@Xbox to get the tools and support you need to unleash your creativity and maximize your success. Before applying to ID@Xbox, please register a developer account on [Windows Dev Center](https://developer.microsoft.com/windows/programs/join).

## Software as a Service for game backend

These are some companies that offer cloud backend for games based on major cloud service providers to allow you to focus on developing your game.

* [GameSparks](http://www.gamesparks.com/)

    GameSparks is a cloud-based development platform for games developers enabling them to build all of their game's server-side

* [Photon Engine](https://www.photonengine.com/en/Photon)

    Photon is an independent networking engine and multiplayer platform for games. It offers Photon Cloud which offers software as a service (SaaS) and as such is a fully managed service. You can completely concentrate on your application client while hosting; server operations and scaling is all taken care of by Exit Games.

* [Playfab](https://playfab.com/)

    Playfab brings world-class live game management and backend technology to your mobile, PC, or console game simply and quickly.

## Related links

* [Windows 10 game development guide](https://msdn.microsoft.com/windows/uwp/gaming/e2e)
* [Azure for gaming](https://azure.microsoft.com/solutions/gaming/)
* [Microsoft BizSpark](https://www.microsoft.com/bizspark/)
* [ID@Xbox](http://www.xbox.com/Developers/id)


 

 
