---
title: Using cloud services for UWP games
description: When developing UWP games across platforms and devices, use a cloud backend to help scale your games according to demand.
ms.assetid: 1a7088e0-0d7b-11e6-8e05-0002a5d5c51b
ms.date: 03/27/2018
ms.topic: article
keywords: windows 10, uwp, games, cloud services
ms.localizationpriority: medium
---
#  Using cloud services for UWP games

The Universal Windows Platform (UWP) in Windows 10 offers a set of APIs that can be used for developing games across Microsoft devices. When developing games across platforms and devices, you can make use of a cloud backend to help scale your game according to demand.

If you are looking for a complete cloud backend solution for your game, see [Software as a Service for game backend](#software-as-a-service-for-game-backend).

##  What is cloud computing?

Cloud computing uses on demand IT resources and applications over the internet to store and process data for your devices. The term _cloud_ is a metaphor for the availability of vast resources out there (not local resources) that you can access from non-specific locations.
The principle of cloud computing offers a new way in which resources and software can be consumed. Users no longer need to pay for the full complete product or resources upfront, but instead are able to consume platform, software, and resources as a service. Cloud providers often bill their customers according to usage or service plan offerings.

##  Why use cloud services?

One advantage of using cloud services for games is that you do not need to invest in physical hardware servers upfront, but only need to pay according to usage or service plans at a later stage. It is one way to help manage the risks involved in developing a new game title. 

Another advantage is that your game can tap into vast cloud resources to achieve scalability (effectively manage any sudden spikes in the number of concurrent players, intense real-time game calculations or data requirements). This keeps the performance of your game stable around the clock. Furthermore, cloud resources can be accessed from any device running on any platform anywhere in the world, which means that you are able to bring your game to everyone globally.

Delivering an amazing gameplay experience to your players is important. Because game servers running in the cloud are independent of client-side updates, they can give you a more controlled and secure environment for your game overall.   You can also achieve gameplay consistency through the cloud by never trusting the client and having server side game logic. Service-to-service connections can also be configured to allow a more integrated gaming experience; examples include linking in-game purchases to various payment methods, bridging over different gaming networks, and sharing in-game updates to popular social media portals such as Facebook and X. 

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
    <tr class="header" align="left">
        <th>Developer</th>
        <th>Description</th>
        <th>Key game scenarios</th>
        <th>Learn more</th>
    </tr>
    <tr>
        <td><a href="https://www.tencent.com">Tencent Games</a></td>
        <td><b>Tencent Games</b> has a developed an innovative solution using Azure Service Fabric enabling traditional PC games to be delivered as a service. Their Cloud Game Solution uses a 'thin client + rich cloud' model running workloads as microservices in the backend.</td>
        <td>
            <ul>
                <li>Traditional PC games are delivered as cloud games to users around the world
                <li>Optimized game delivery process
                <li>Game functionalities isolated as microservices to reduce complexity, reduce workloads repetition due to dependencies, and ability to upgrade new features independently
                <li>Small installation package downloads to play newest game content (Reduced package size from GB to MB)
                <li>Reduced maintenance cost
            </ul>
        </td>
        <td>
            <ul>
                <li><a href="https://customers.microsoft.com/story/tencent-telecommunications-azure-service-fabric-windows-server-en">Tencent Games and Microsoft built the cloud game solution</a></li>
                <li>Building Games with Service Fabric: Details about Tencent's implementation (video)</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td><a href="https://www.halowaypoint.com/">343 Industries</a></td>
   <td><b>Halo 5: Guardians</b> implemented Halo: Spartan Companies as its social gameplay platform by using Azure Cosmos DB (via DocumentDB API), which was selected for its speed and flexibility due to its auto-indexing capabilities.</td>
        <td>
            <ul>
                <li>Scalable data-tier to handle groups creation/management for multiplayer gameplay
                <li>Game and social media integration
                <li>Real-time queries of data through multiple attributes
                <li>Synchronization of gameplay achievements and stats
            </ul>
        </td>
    </tr>
    <tr>
        <td><a href="http://www.illyriad.co.uk/">Illyriad Games</a></td>
        <td>Illyriad Games created <b>Age of Ascent</b>, a massively multiplayer online (MMO) epic 3D space game that can be played on devices that have modern browsers. So this game can be played on PCs, laptops, mobile phones and other mobile devices without plug-ins. The game uses ASP.NET Core, HTML5, WebGL, and Azure.</td>
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
                <li>Building games with Service Fabric: Age of Ascent MMO game (video)
                <li>Manage game components as microservices using Azure Service Fabric (video) 
             <li><a href="https://www.youtube.com/watch?v=XaN-eXkIEbM">Interview with Age of Ascent developers (video)</a>
            </ul>
        </td>
    </tr>
    <tr>
        <td><a href="https://www.nextgames.com/">Next Games</a></td>
        <td>Next Games is the creator of <b>The Walking Dead: No Man's Land</b> video game which is based on AMC's original series. The Walking Dead game used Azure as the backend. It had 1,000,000 downloads in the opening weekend and within the first week, the game became #1 iPhone & iPad Free App in the U.S. App Store, #1 Free App in 12 countries, and #1 Free Game in 13 countries.
        </td>
        <td>
            <ul>
                <li>Cross-platform
                <li>Turn based multiplayer
                <li>Elastically scale performance
                <li>Gamer fraud protection
                <li>Dynamic content delivery
            </ul>
        </td>
        <td>
            <ul>
                <li><a href="https://azure.microsoft.com/blog/how-we-built-it-next-games-global-online-gaming-platform-on-azure/">How we built it: Next Games global online gaming platform on Azure (blog with video)</a>
            </ul>
        </td>
    </tr>
    <tr>
        <td><a href="http://www.crimecoast.com/">Pixel Squad</a></td>
        <td>Pixel Squad developed <b>Crime Coast</b> using Unity game engine and Azure. <b>Crime Coast</b> is a social strategy game available on the Android, iOS and Windows platform. Azure Blob Storage, Managed Azure Redis Cache, an array of load balanced IIS VMs, and Microsoft Notification hub were used in their game. Learn how they managed scaling and handled players surge with 5000 simultaneous players.
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
                <li>How Crime Coast MMO game used Azure Cloud Services
            </ul>
        </td>
    </tr> 
</table>

    
### Other links

* Hitman and Azure: Create game features like Elusive Target that are only possible using cloud
* [Azure as the secret sauce for Hitcents, Game Troopers and InnoSpark](https://news.microsoft.com/features/game-developers-use-microsoft-azure-as-secret-sauce-for-scale-and-growth-2/)


## How to design your cloud backend

While producers and game designers are in discussion about what game features and functionalities are needed in the game, it is good to start considering how you want to design your game infrastructure. Azure can be used as your game backend when you want to develop games for various devices and across different major platforms.

### Understanding IaaS, PaaS or SaaS

First, you need to think about the level of service that is best suited for your game. Knowing the differences in the following three services can help you determine the approach you want to take in building your backend.

* [Infrastructure as a Service (IaaS)](https://azure.microsoft.com/overview/what-is-iaas/)

    Infrastructure as a Service (IaaS) is an instant computing infrastructure, provisioned and managed over the Internet. Imagine having the possibility of many machines readily available to quickly scale up and down depending on demand. IaaS helps you to avoid the cost and complexity of buying and managing your own physical servers and other datacenter infrastructure.

* [Platform as a Service (PaaS)](https://azure.microsoft.com/overview/what-is-paas/)

    Platform as a Service (PaaS) is like IaaS but it also includes management of infrastructure like servers, storage, and networking. So on the top of not buying physical servers and datacenter infrastructure, you also do not need to buy and manage software licenses, underlying application infrastructure, middleware, development tools, or other resources.

* [Software as a Service (SaaS)](https://azure.microsoft.com/overview/what-is-saas/)

    Software as a service (SaaS) allows users to connect to and use cloud-based apps over the Internet. It provides a complete software solution that you purchase on a pay-as-you-go basis from a cloud service provider.  Common examples are email, calendaring, and office tools (such as Microsoft 365 Office apps). You rent the use of an app for your organization, and your users connect to it over the Internet, usually with a web browser. All of the underlying infrastructure, middleware, app software, and app data are located in the service provider's data center. The service provider manages the hardware and software, and with the appropriate service agreement, will ensure the availability and the security of the game and your data as well. SaaS allows your organization to get quickly up and running with an app at minimal upfront cost.


### Design your game infrastructure using Azure

Following are some ways that Azure cloud offerings can be used for a game. Azure works with Windows, Linux, and familiar open source technologies such as Ruby, Python, Java, and PHP. For more information, see [Azure for gaming](https://azure.microsoft.com/solutions/gaming/).

| Requirements                 | Activity scenarios                            | Product Offering                      | Product Capabilities                                    |
|-----------------------------------|-----------------------------------------------|---------------------------------------|----------------------------------------------------|
| Host your domain in the cloud     | Respond to DNS queries efficiently            | [Azure DNS](https://azure.microsoft.com/services/dns/) | Host your domain with high performance and availability    |
| Sign in, identity verification      | Gamer signs in and gamer identity is authenticated  | [Azure Active Directory](https://azure.microsoft.com/services/active-directory/) | Single sign-on to any cloud and on-premises web app with multi-factor authentication            | 
| Game using infrastructure as a service model (IaaS)      | Game is hosted on virtual machines in the cloud       | [Azure VMs](https://azure.microsoft.com/services/virtual-machines/) | Scale from 1 to thousands of virtual machine instances as game servers with built-in virtual networking and load balancing; hybrid consistency with on-premises systems           |
| Web or mobile games using platform as a service model (PaaS)            | Game is hosted on a managed platform                | [Azure App Service](https://azure.microsoft.com/services/app-service/) | PaaS for websites or mobile games (which means Azure VMs with middleware/development tools/BI/DB management)   |
| Highly available, scalable n-tier cloud game with more control of the OS  (PaaS)        | Game is hosted on a managed platform                | [Azure Cloud Service](https://azure.microsoft.com/services/app-service/) | PaaS designed to support applications that are scalable, reliable, and cheap to operate   |
| Load balancing across regions for better performance and availability | Routes incoming game requests. Can act as first level of load balancing.       | [Azure Traffic Manager](https://azure.microsoft.com/services/traffic-manager/) | Offers multiple automatic failover options and ability to distribute your traffic equally or with weighted values. Can seamlessly combine on-premises and cloud systems. |
| Cloud storage for game data       | Latest game data is stored in the cloud and sent to client devices | [Azure Blob Storage](https://azure.microsoft.com/services/storage/blobs/)| No restriction on the kinds of file that can be stored; object storage for large amounts of unstructured data like images, audio, video, and more.  |
| Temporary data storage tables| Game transactions (changes in game states) are stored in tables temporarily | [Azure Table Storage](https://azure.microsoft.com/services/storage/tables/)| Game data can be stored in a flexible schema according to the needs of the game |
| Queue game transactions/requests| Game transactions are processed in the form of a queue | [Azure Queue Storage](https://azure.microsoft.com/services/storage/queues/)| Queues absorb unexpected traffic bursts and can prevent servers from being overwhelmed by a sudden flood of requests during the game   |
| Scalable relational game database| Structured storage of relational data like in-game transactions to database | [Azure SQL Database](https://azure.microsoft.com/services/sql-database/)| SQL database as a service ([Compare with SQL on a VM](/azure/azure-sql/azure-sql-iaas-vs-paas-what-is-overview))  |
| Scalable distributed low-latency game database| Fast read, write, and query of game and player data with schema flexibility | [Azure Cosmos DB](https://azure.microsoft.com/services/cosmos-db/)| Low latency NoSQL document database as a service   |
| Use own datacenter with Azure services | Game is retrieved from your own datacenter and sent to the client devices | [Azure Stack](https://azure.microsoft.com/overview/azure-stack/) | Enables your organization to deliver Azure services from your own datacenter to help you achieve more  |
| Large data chunks transfer| Large files such as game images, audio, and videos can be sent to users from the nearest Content Delivery Network (CDN) pop location with Azure CDN     | [Azure Content Delivery Network](https://azure.microsoft.com/services/cdn/) | Built on a modern network topology of large centralized nodes, Azure CDN handles sudden traffic spikes and heavy loads to dramatically increase speed and availability, resulting in significant user experience improvements  |
| Low latency               | Perform caching to build fast, scalable games with more control and guaranteed isolation of data; can be used to improve match-making feature for game as well. | [Azure Redis Cache](https://azure.microsoft.com/services/cache/) | High throughput, consistent low-latency data access to power fast, scalable Azure applications  |
| High scalability, low latency | Handles fluctuations in the number of game users with low latency read and writes | [Azure Service Fabric](https://azure.microsoft.com/services/service-fabric/) | Able to power the most complex, low-latency, data-intensive scenarios and reliably scale to handle more users at a time. Service Fabric enables you to build games without having to create a separate store or cache, as required for stateless apps |
| Ability to collect millions of events per second from devices                         | Log millions of events per second from devices | [Azure Event Hubs](https://azure.microsoft.com/services/event-hubs/) | Cloud-scale telemetry ingestion from games, websites, apps, and devices  |
| Real time processing for game data  | Perform real-time analysis of gamer data to improve gameplay| [Azure Stream Analytics](https://azure.microsoft.com/services/stream-analytics/) | Real-time stream processing in the cloud  |
| Develop predictive gameplay          | Create customized dynamic gameplay based on gamer data    | [Azure Machine Learning](https://azure.microsoft.com/services/machine-learning/) | A fully managed cloud service that enables you to easily build, deploy, and share predictive analytics solutions  |
| Collect and analyze game data| Massive parallel processing of data from both relational and non-relational databases | [Azure Data Warehouse](https://azure.microsoft.com/services/sql-data-warehouse/)| Elastic data warehouse as a service with Enterprise class features   |
| Engage users to increase usage and retention| Send targeted push notifications to any platform from any back end to generate interest and encourage specific game actions | [Azure Notification Hubs](https://azure.microsoft.com/services/notification-hubs/)| Fast broadcast push to reach millions of mobile devices on all major platforms &mdash; iOS, Android, Windows, Kindle, Baidu. Your game can be hosted on any back end &mdash; cloud or on-premises.|
| Stream media content to your local and worldwide audiences while protecting your content| Broadcast quality game trailers and cinematic clips can be watched from all devices| [Azure Media Services](https://azure.microsoft.com/services/media-services/)| On-demand and live video streaming with integrated Content Delivery Network capabilities. Use one player for all of your playback needs, includes content protection and encryption.| 
| Develop, distribute, and beta-test your mobile apps | Test and distribute your mobile app. App performance and user experience management. | [HockeyApp](https://azure.microsoft.com/services/hockeyapp/)| Integrates crash reporting and user metrics with an app distribution and user feedback platform. Supports Android, Cordova, iOS, OS X, Unity, Windows, and Xamarin apps. Also, consider [Visual Studio Mobile Center](https://visualstudio.microsoft.com/app-center/) &mdash; mission control for apps that combines rich analytics, crash reporting, push notifications, app distribution, and more. |
| Create marketing campaigns to increase usage and retention  |    Send push notifications to targeted players to generate interest and encourage specific game actions according to data analysis | [Mobile engagement](https://azure.microsoft.com/services/mobile-engagement/) - will be retired March 2018 and is currently only available to existing customers |  Increase gameplay time and user retention on all major platforms—iOS, Android, Windows, Windows Phone |


##  Startup and developer resources

* [Microsoft for Startups](https://startups.microsoft.com)

    Microsoft for Startups provides product, technical, and go-to-market benefits to help accelerate the growth of startups. One benefit includes getting an Azure free account. You have $200 credit to explore services for 30 days, 12 months of popular free services, and always free 25+ services. For more information, see [Bring your startup's ideas to life with an Azure free account](https://azure.microsoft.com/free/startups/).
    
* [Developer programs](e2e.md#developer-programs)

    Microsoft offers several developer programs like [ID@Xbox](https://www.xbox.com/Developers/id) and [Xbox Live Creators Program](https://developer.microsoft.com/games/publish/) to help you develop and publish games.

## Learning resources

* //build 2016: [CodeLabs &mdash; Use Microsoft Azure App Service and Microsoft SQL Azure backend to save game score in Unity](https://github.com/Microsoft-Build-2016/CodeLabs-GameDev-6-Azure)
* //build 2017: Delivering world-class game experiences using Microsoft Azure: Lessons learned from titles like Halo, Hitman, and Walking Dead (video)
* Reusable set of building blocks, projects, services, and best practices designed to support common gaming workloads using Azure on GitHub: [Building blocks for gaming on Azure](https://github.com/MicrosoftDX/nether)
* Gaming Services on Azure (videos)

## Tools and other useful links

* [MSDN forums &mdash; Azure platform](https://social.msdn.microsoft.com/Forums/azure/home?category=windowsazureplatform)
* [Cloud Based Load Testing tool](https://visualstudio.microsoft.com/team-services/cloud-load-testing/)
* [SDKs and command-line tools](https://azure.microsoft.com/downloads/)
    
## Software as a Service for game backend

[Azure PlayFab](https://playfab.com) currently powers more than 1,200 live games with 80 million monthly active players. It's a complete backend platform that includes full stack LiveOps with real-time control. 

You can integrate this solution into your mobile, PC, or console games using SDKs. There are SDKs available for all popular game engines and platforms, including Android, iOS, Unreal, Unity, and Windows.

It offers game services like authentication, player data management, multiplayer, and real-time analytics to help your game grow its user base. Harness the power of real-time data pipeline and LiveOps to engage your users with customized in-game items, events, and promotions. You also have the ability to conduct A/B testing, generate reports, send push notifications, and more. 

We're constantly innovating and adding new features. For more information, see [Azure PlayFab](https://playfab.com); and for pricing, see [Pricing](https://playfab.com/pricing).

## Related links

* [Windows game development guide](./e2e.md)
* [Azure for gaming](https://azure.microsoft.com/solutions/gaming/)
* [Azure PlayFab](https://playfab.com)
* [Microsoft for Startups](https://startups.microsoft.com)
* [ID@Xbox](https://www.xbox.com/Developers/id)
