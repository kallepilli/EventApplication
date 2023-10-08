RAKENDUSE JOOKSUTAMINE:
1. vueapp directorys sisestada terminali käsklus "npm install"
2. webapi directorys jooksutada dockeri faili käsklusega "docker-compose up", et seada üles lokaalne PostgreSQL andmebaas
	2.1 pgAdmin andmebaasihaldur asub http://localhost:5050/browser/
	2.1 Lisada server ning hostiks määrata "postgres" (port 5432). Username: "postgres", password: "password"
3. Visual studios jooksutada rakendus (Multiple startup projects määrata webapi ning vueapp). 
	3.1 Oodata hetk, et käivituks ka webapi koos swaggeriga. Toimub ka testandmete sisestamine. Värskendada vueapp-i, kuni ürituse loetelusse lisanduvad üritused.
4. Rakendus on valmis töötamiseks.

Back-end (webapi)
	* Back-end rakenduse API Swagger asub https://localhost:7165/swagger/index.html
	* Loodud .NET raamistikus kasutades C# programmeerimiskeelt.
	* Andmebaasina kasutatakse PostgreSQL andmebaasi
	* Back-end on jaotatud viide kihti:
		1) Aids - sisaldab helper meetodeid
		2) Controllers - võimaldab kirjeldatud meetodite abil kliendil ja front-end rakendusel suhelda back-endiga pöördudes Service kihi poole
		3) Services - sisaldab suuremat osa back-end loogikast. Service on nii-öelda vahelüli Controlleri ja Repository vahel. Saadab andmeid edasi-tagasi neid vahepeal vajadusel töödeldes
		4) Repositories - sisaldab meetodeid, mis suhtlevad andmebaasiga. Põhiliselt CRUD funktsioonid. Suhtleb omakorda Service kihis asuvate olemitega
		5) Data - sinna kihti on koondatud klassid, mida back-end rakenduses kasutatakse. Lisaks vastutab Data kiht andmebaasi loomise ja selle struktuuri eest

Front-end (vueapp)
	* Front-end rakendus asub https://localhost:5173/
	* Loodud Vue.js raamistikus kasutades TypeScript programmeerimiskeelt
	* Rakendus on jaotatud samuti viide kihti:
		1) Router - vastutab rakenduses navigeerimise eest
		2) Assets - sisaldab erinevaid meediafaile ning ka style.css faili
		3) Compoonents - sisaldab korduvkasutatavaid front-end komponente
		4) Views - sisaldab erinevaid kliendile kuvatavaid vaateid
		5) Model - sisaldab kirjeldatud liideseid, mida rakenduses kasutatakse

TESTID:
Back-end on testitud ühiktestidega.
	* Unit testid asuvad projektis nimega ApiUnitTests ning on jaotatud kaustadesse järgides webapi arhitektuuri.
		1) AidsTests
		2) ControllerTests
		3) RepositoryTests
		4) ServiceTests


Prioriteedid rakenduse edaspidiseks testimiseks:
	1) Seada üles pipeline, et pärast commiti jooksutada testid ning vältida vigase rakenduse ühildamist master haruga
	2) Lisada API Endpoint testid
	3) Lisada front-end testid



