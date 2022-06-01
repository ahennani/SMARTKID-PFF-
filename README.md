<h1>SMARTKID (--PEF--)</h1>

- [Project Context](#project-context).

- [The Idea of the Application](#the-idea-of-the-application).

- [Objective and Motivation](#objective-and-motivation).

- [Application Features](#application-features).
    -  External Login (Facebook, Twitter and Google).
    - Contacts.
    - SignUp / SignIn.
    - Manipulate Files.

- [Conception](#Conception).
    -  Introduction.
    - MCD.
    - MLD.

- [Technologies and Environment](#technologies-and-environment):
    - Introduction.
    - Hardware Environment
    - Software Environment
    - Technologies (Back-End)
    - Libraries (Front-End)

- [Realization (Screenshots)](#realization):
    - First Interface (Home Page).
    - SignIn Page.
    - Contact Page.
    - Inscription Page.
    - Responsive Design.

***
## Project Context
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;As part of the Studies to get the diploma of “Technicien spécialisé” it was necessary to create an App based on what we have learned in the institution and it’s a good way to improve the knowledge about how Apps works on the other side and learn how to face the problems during the production process and the ability to describe the application in a report.
Besides that, learning the future technologies and getting used to it, by making this project with one of the most recent technology of Microsoft ASP.NET Core.

## The Idea of the Application
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The purpose of this type of website is to uphold the overall brand of the kindergarten or school. A higher education website contains a great deal of important information for parents, school and staff. Additionally, it’s a way of representing the environment culture for prospective parents.
This website will help parents to register their children, also can pay their monthly fees. In addition, they could stay in touch with teachers and watchers so they get to know if their children are safe or got a bad habit.

## Objective and Motivation 

<details>
<summary><b>Objective</b></summary>
<p>
My objective was to practice C# programming language and get a clear about vision Microsoft technologies, also prepare myself for being a Web Developer.
</p>
</details>

<details>
<summary><b>Motivation</b></summary>
<p>
I was motivated at the first place because of the diploma program which it contains C# with .NET Framework, so I started learning it for that. Later I discovered my passion and ambition for more and new things about how Web Apps work. Then it ends up developing this website and looking forward to improve it more with new features and publish it or give it a real kindergarten. 
</p>
</details>

## Application Features 

<details>
<summary><b>External Login (Facebook, Twitter and Google)</b></summary>
<p>
    External Login allows you to login to your website site using an External Database instead of your database. This means if you already have a login system you can integrate that into your website site. The External Database that you would like to use does not have to be a website database.
</p>
</details>


<details>
<summary><b>Contacts</b></summary>
<p>
System to listen to customers and parents their needs and try to implement it in the next updates. Also help those who needs it to choose a good program for them and their children. 
</p>
</details>

<details>
<summary><b>SignUp/SignIn</b></summary>
<p>
The website also provides a way to keep in touch with the teachers and watchers for staying up to date children conditions and needs. 
</p>
</details>

<details>
<summary><b>Manipulate Files</b></summary>
<p>
In addition to login system in the previous aspects, also we upload pictures and files to our server so we can confirm the inscription after the guardians comes to us in order to validate it and pay their inscription fees.
</p>
</details>


##  Conception

 <details>
<summary><b>Introduction</b></summary>
<p>
 Conception is a preliminary and essential stage that must precede the development stage of any IT application. To describe the design of our website we will use MERISE which is a method of Conception, development and realization of IT projects databases.
</p>
</details>

 <details>
<summary><b>MCD</b></summary>
<p>
 In The MCD is a high-level graphical representation that allows easy and simple to understand how the various elements are interconnected using coded diagrams with the following part:
    - Entities (1 rectangle = 1 object).
    - Properties (the list of entity data).
    - Cardinalities.

![MCD Giagram](/..Screenshot/mcd.png)

</p>
</details>

 <details>
<summary><b>MLD</b></summary>
<p>
Uses the content of the previous MCD, but specifies the volume, structure and organization of the data as they can be implemented. For example, at this stage, it is possible to know the exhaustive list of tables that will be created in a relational database.

![MLD Diagram](/..Screenshot/mld.png)

</p>
</details>


##  Technologies and Environment

<details>
<summary><b>Introduction </b></summary>
<p>
To be able to develop a Web Application, it is necessary to choose technologies that make it possible to simplify its implementation. For that, after having completed the conceptual study, we will approach the implementation part in what follows. We start by presenting the hardware and software environment, and then the implementation status
</p>
</details>

<details>
<summary><b>Hardware Environment</b></summary>
<p>
For the realization of the application I used PC:

    - Intel Core i5/5th Generation 2.30 GHz. 
    - 8 Go RAM.
    - Windows 10 - 64 bits.
</p>
</details>

<details>
<summary><b>Software Environment</b></summary>
<p>

1. Visual Studio Community 2019.
2. Visual Studio Code
3.	SQL Server 2019.
4.	Notepad++.

</p>
</details>

<details>
<summary><b>Technologies (Back-End)</b></summary>
<p>

1.	ASP.NET CORE (MVC Pattern).
2.	ASP.NET CORE IDENTITY.
3.	ENTITY FRAMEWORK Core (Code First Approach).
4.	NLog Web: 
    - In real world applications a proper error logging mechanism is essential to track and troubleshoot the unexpected behavior of the application.

</p>
</details>

<details>
<summary><b>Libraries (Front-End)</b></summary>
<p>

1.	JQuery:
2.	Owl Carousel: 
3.	Bootstrap 4: 
4.	Fontawesome 5 (Free Edition): 
5.	Isotope: 

</p>
</details>

##  Realization

<details>
<summary><b>First Interface (Home Page)</b></summary>
<p>
This interface is the home page of the site containing in the middle a "Carousel" which displays photos related to the absence, there is also a bar at the top of the page which contains a series of links among them, Link to Home Page which is used to return to the home page, Contact Link which takes the user to the contact page and lastly SignIn which takes the user to the authentication page and also link to Inscription page.
<br />

<!-- ![Home Page](/..Screens/HomePage.png) -->

<img src="/..Screens/HomePage.png" width="100%" />

</p>
</details>

<details>
<summary><b>SignIn Page </b></summary>
<p>
Allows you to sign in with your account.
<br />

<img src="/..Screens/SignInPage.png" width="100%" />
</p>
</details>

<details>
<summary><b>Contact Page </b></summary>
<p>
Allows you to send feedback to website owner.
<br />

<img src="/..Screens/ContactPage.png" width="100%" />
</p>
</details>

<details>
<summary><b>Inscription Page</b></summary>
<p>
Allows you to register a child in the school.
<br />

<img src="/..Screens/InscriptionPage.png" width="100%" />
</p>
</details>

<details>
<summary><b>Responsive Design</b></summary>
<p>
Some pages from the website and what it looks in the mobile phone.
<br />

<img src="/..Screens/ResponsivePages.png" width="100%" />
</p>
</details>
