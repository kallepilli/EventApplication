RAKENDUSE JOOKSUTAMINE:
1. vueapp directorys sisestada terminali k�sklus "npm install"
2. webapi directorys jooksutada dockeri faili k�sklusega "docker-compose up", et seada �les lokaalne PostgreSQL andmebaas
	2.1 pgAdmin andmebaasihaldur asub http://localhost:5050/browser/
	2.1 Lisada server ning hostiks m��rata "postgres" (port 5432). Username: "postgres", password: "password"
3. Visual studios jooksutada rakendus (Multiple startup projects m��rata webapi ning vueapp). 
	3.1 Oodata hetk, et k�ivituks ka webapi koos swaggeriga. Toimub ka testandmete sisestamine. V�rskendada vueapp-i, kuni �rituse loetelusse lisanduvad �ritused.
4. Rakendus on valmis t��tamiseks.

Back-end (webapi)
	* Back-end rakenduse API Swagger asub https://localhost:7165/swagger/index.html
	* Loodud .NET raamistikus kasutades C# programmeerimiskeelt.
	* Andmebaasina kasutatakse PostgreSQL andmebaasi
	* Back-end on jaotatud viide kihti:
		1) Aids - sisaldab helper meetodeid
		2) Controllers - v�imaldab kirjeldatud meetodite abil kliendil ja front-end rakendusel suhelda back-endiga p��rdudes Service kihi poole
		3) Services - sisaldab suuremat osa back-end loogikast. Service on nii-�elda vahel�li Controlleri ja Repository vahel. Saadab andmeid edasi-tagasi neid vahepeal vajadusel t��deldes
		4) Repositories - sisaldab meetodeid, mis suhtlevad andmebaasiga. P�hiliselt CRUD funktsioonid. Suhtleb omakorda Service kihis asuvate olemitega
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
Back-end on testitud �hiktestidega.
	* Unit testid asuvad projektis nimega ApiUnitTests ning on jaotatud kaustadesse j�rgides webapi arhitektuuri.
		1) AidsTests
		2) ControllerTests
		3) RepositoryTests
		4) ServiceTests


Prioriteedid rakenduse edaspidiseks testimiseks:
	1) Seada �les pipeline, et p�rast commiti jooksutada testid ning v�ltida vigase rakenduse �hildamist master haruga
	2) Lisada API Endpoint testid
	3) Lisada front-end testid



