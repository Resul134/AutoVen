import axios, {
    AxiosResponse,
    AxiosError
} from "../../node_modules/axios/index"; // Don't worry
import * as am4core from "../../node_modules/@amcharts/amcharts4/core";
import * as am4charts from "../../node_modules/@amcharts/amcharts4/charts";


var MyStorage = window.localStorage;

interface Logging {
    id: number;
    dato: Date;
    luftfugtighed: number;
    aktiv: boolean;
    uLuftfugtighed: number;
}

interface Status {
    id: number;
    dato: Date;
    allowChange: boolean;
}

interface chartTemplate {
    date: Date;
    luftfugtighed: number;
}

//henter den nyeste entry fra databasen
let urlgetLast: string = "https://autovenrest.azurewebsites.net/api/Logging/getLast";

let urlLogPost: string = "https://autovenrest.azurewebsites.net/api/Logging/";

let statusURI: string = "https://autovenrest.azurewebsites.net/api/Status/1/";
let tilstandUrl: string = "https://autovenrest.azurewebsites.net/api/Status/";

let humidInside: HTMLDivElement = <HTMLDivElement>document.getElementById("humidInside")

//Humidtiy
let humidOutput: HTMLDivElement = <HTMLDivElement>document.getElementById("humidOutput")


let urlHumid: string = "https://api.openweathermap.org/data/2.5/weather?q=Roskilde,DK&APPID=f0f18be3bdaa24b17b922c92f5cd9279"

//Login

let loginBut: HTMLButtonElement = <HTMLButtonElement>document.getElementById("buttonLogin");
if (loginBut != null) {
    loginBut.addEventListener("click", loginFuc)
}

let logUDBut: HTMLButtonElement = <HTMLButtonElement>document.getElementById("buttonLogud")
if (logUDBut != null) {
    logUDBut.addEventListener("click", logudFunc)
}

let bruger: HTMLInputElement = <HTMLInputElement>document.getElementById("email");
let kodeord: HTMLInputElement = <HTMLInputElement>document.getElementById("kodeord");
let logOutput: HTMLDivElement = <HTMLDivElement>document.getElementById("loginValidation");

let aktivitet: HTMLSpanElement = <HTMLSpanElement>document.getElementById("status")
let fanblade: HTMLImageElement = <HTMLImageElement>document.getElementById("fan");

let turnOnButton: HTMLButtonElement = <HTMLButtonElement>document.getElementById("onButton");
let turnOffButton: HTMLButtonElement = <HTMLButtonElement>document.getElementById("offButton");
let automatiskButton: HTMLButtonElement = <HTMLButtonElement>document.getElementById("autoButton");

if (automatiskButton != null) {
    automatiskButton.addEventListener("click", auto)
}

if (turnOnButton != null) {
    turnOnButton.addEventListener("click", turnOn)
}

if (turnOffButton != null) {
    turnOffButton.addEventListener("click", turnOff)
}

function getDate(): String {
    let thisDate: Date = new Date();
    let options = { day: "2-digit" }
    let currentHours = thisDate.getHours().toString();
    currentHours = ("0" + currentHours).slice(-2);
    let currentMin = (thisDate.getMinutes()<10?'0':'') + thisDate.getMinutes();
    let currentSec = (thisDate.getSeconds()<10?'0':'') + thisDate.getSeconds();
    let dateString: string = thisDate.getFullYear() + "-" + (thisDate.getMonth() + 1) + "-" + thisDate.toLocaleDateString("en-US", options) + "T" + currentHours + ":" + currentMin + ":" + currentSec;

    return dateString;
}

function postLog(status: boolean) {
    axios.get<Logging>(urlgetLast)
        .then((response: AxiosResponse<Logging>) => {
            let dataOne: Logging = response.data;
            axios.post<Logging>(urlLogPost, { dato: getDate(), luftfugtighed: dataOne.luftfugtighed, aktiv: status, uLuftfugtighed: dataOne.uLuftfugtighed }).then(() => {
                getLatestLog();
                getAllLogs();
                getAllActivities();
                GetTilstand();
            })
        })
}

function turnOn() {
    axios.put<Status>(statusURI, { id: 1, dato: getDate(), allowChange: false })
        .then((response: AxiosResponse) => {
            postLog(true);
        })
}

function turnOff() {
    axios.put<Status>(statusURI, { id: 1, dato: getDate(), allowChange: false })
        .then((response: AxiosResponse) => {
            postLog(false);
        }).catch(err => {
            console.log(err.response)
        })
}

function auto() {
    axios.put<Status>(statusURI, { id: 1, dato: getDate(), allowChange: true })
        .then((response: AxiosResponse) => {
            getLatestLog()
            GetTilstand()
        })
}

function getHumid(): void {
    axios.get(urlHumid)
        .then((main: AxiosResponse) => {
            let data = main.data["main"]["humidity"];
            let longHtml: string = "<p>";

            longHtml += "Fugtighed udenfor: " + data + "%";

            longHtml += "</p>";
            humidOutput.innerHTML = longHtml;
        })
        .catch((error: AxiosError) => {
            console.log(error);
            humidOutput.innerHTML = error.message;
        });
}


function getLatestLog(): void {
    axios.get<Logging>(urlgetLast)
        .then((response: AxiosResponse<Logging>) => {
            let dataOne: Logging = response.data;

            let longHtml2: string = "<p>";

            if (dataOne.aktiv) {
                aktivitet.className = "on";
                aktivitet.innerHTML = "Tændt"
                fanblade.className = "rotating center";
            }
            else {
                aktivitet.className = "off";
                aktivitet.innerHTML = "Slukket"
                fanblade.className = "center";
            }
            humidInside.innerHTML = JSON.stringify(dataOne);

            longHtml2 += "Fugtighed indenfor: " + dataOne.luftfugtighed.toPrecision(3) + "%";

            longHtml2 += "</p>";
            humidInside.innerHTML = longHtml2;
        });
}

function logudFunc(): void {

    MyStorage.removeItem('logged');
    location.href = "index.htm";
}

function loginFuc(): void {
    let loginBruger = bruger.value;
    let loginKodeord = kodeord.value;
    let admindUser: String = "Admin";
    let AdminPass: String = "Admin";

    let listError: string[] = [
        'Forkert din bøv', 'Try again', 'Come on now bro',
        'You suck at logging in', 'Try again..', 'Login failed',
        'Are you really trying to login?', 'Houston we have a problem',
        'Username or password is incorrect'
    ]
    var randomError = listError[Math.floor(Math.random() * listError.length)];

    if (loginBruger != null && loginKodeord != null) {

        try {
            if (admindUser == loginBruger && AdminPass == loginKodeord) {
                MyStorage.setItem('logged', 'true')
                logOutput.innerHTML = "Success";
                location.href = "mainsite.htm";
            }
            else {
                logOutput.innerHTML = randomError;
            }
        }
        catch (e) {
            if (e instanceof Error) {
                logOutput.innerHTML = "Something went wrong: " + e.message;
            }
            if (function (error: AxiosError): void {
                logOutput.innerHTML = error.message;
            })
                throw e;
        }
        finally {

            if (admindUser != loginBruger && AdminPass != loginKodeord) {
                logOutput.innerHTML = randomError;
                bruger.value = "";
                kodeord.value = "";
            }
        }
    }
}

function timer(): void {
    let b = true;
    setInterval(function () {
        let t = new Date().getMinutes();
        if (t == 1 || t == 16 || t == 31 || t == 46) {
            if (b) {
                //Kode til at tjekke status og stoppe/starte blæserbladet her
                getLatestLog();
                getHumid();
                getAllActivities();
                getAllLogs();

                console.log(t);
                b = false;
            }
        } else {
            if (!b) b = true;
        }
    }, 1000)
}

let chart: any;
let newchart: any;

//Kører kun på main siden, metoder der altid kører på main
if (window.location.pathname.toString().indexOf("mainsite.htm") >= 0) {
    timer();
    getHumid();
    getLatestLog();
    GetTilstand();

    chart = am4core.create("chartdiv", am4charts.XYChart);
    chart.paddingRight = 20;
    chart.events.on("ready", function() {
        let before = new Date();
        let now = new Date();
        dateAxis.zoomToDates(
            before.setDate(before.getDate() - 1),
            now.setDate(now.getDate())
        );
    })

    let dateAxis = chart.xAxes.push(new am4charts.DateAxis());
    dateAxis.baseInterval = {
        "timeUnit": "minute",
        "count": 1
    };
    dateAxis.renderer.grid.template.location = 0;
    dateAxis.renderer.minGridDistance = 50;
    dateAxis.tooltipDateFormat = "HH:mm, d MMMM";

    let valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
    valueAxis.tooltip.disabled = true;
    valueAxis.title.text = "Luftfugtighed";
    valueAxis.renderer.minGridDistance = 20;
    valueAxis.min = 0;

    let series = chart.series.push(new am4charts.LineSeries());
    series.dataFields.dateX = "dato";
    series.dataFields.valueY = "luftfugtighed";
    series.tooltipText = "Luftfugtighed: [bold]{valueY}[/]%";
    series.fillOpacity = 0;
    series.strokeWidth = 2;

    let useries = chart.series.push(new am4charts.LineSeries());
    useries.dataFields.dateX = "dato";
    useries.dataFields.valueY = "uLuftfugtighed";
    useries.tooltipText = "Udendørs Luftfugtighed: [bold]{valueY}[/]%";
    useries.fillOpacity = 0;
    useries.stroke = am4core.color("#ff0000");
    useries.fill = am4core.color("#ff0000");
    useries.strokeWidth = 2;
    

    chart.cursor = new am4charts.XYCursor();
    chart.cursor.lineY.opacity = 0;
    dateAxis.start = 0;
    dateAxis.keepSelection = true;

    getAllLogs();

    newchart = am4core.create("newchartdiv", am4charts.XYChart);
    newchart.paddingRight = 20;
    
    newchart.events.on("ready", function() {
        let before = new Date();
        let now = new Date();
        newdateAxis.zoomToDates(
            before.setDate(before.getDate() - 1),
            now.setDate(now.getDate())
        );
    })
    

    let newdateAxis = newchart.xAxes.push(new am4charts.DateAxis());
    newdateAxis.baseInterval = {
        "timeUnit": "minute",
        "count": 1
    };
    newdateAxis.renderer.grid.template.location = 0;
    newdateAxis.renderer.minGridDistance = 50;
    newdateAxis.tooltipDateFormat = "HH:mm, d MMMM";

    let newvalueAxis = newchart.yAxes.push(new am4charts.ValueAxis());
    newvalueAxis.tooltip.disabled = true;
    newvalueAxis.title.text = "Aktivitet";
    newvalueAxis.max = 1;
    newvalueAxis.paddingRight = 15;
    newvalueAxis.renderer.minGridDistance = 100;

    let newseries = newchart.series.push(new am4charts.LineSeries());
    newseries.dataFields.dateX = "dato";
    newseries.dataFields.valueY = "aktiv";
    newseries.fillOpacity = 0;
    newseries.strokeWidth = 2;

    newchart.cursor = new am4charts.XYCursor();
    newchart.cursor.lineY.opacity = 0;
    newdateAxis.start = 0;
    newdateAxis.keepSelection = true;

    getAllActivities();    
}

function getAllLogs() {
    let getData = [];
    axios.get<[Logging]>(urlLogPost)
        .then((response: AxiosResponse<[Logging]>) => {
            response.data.forEach(element => {
                delete element["aktiv"]
                delete element["id"];
                let tempDate = new Date(element.dato.toString());
                element.dato = tempDate
            })

            getData = response.data;
            chart.data = getData;
            console.log(getData)
        });
}

function getAllActivities() {
    let getData = [];
    axios.get<[Logging]>(urlLogPost)
        .then((response: AxiosResponse<[Logging]>) => {
            response.data.forEach(element => {
                delete element["luftfugtighed"]
                delete element["id"];
                delete element["uLuftfugtighed"];
                let tempDate = new Date(element.dato.toString());
                element.dato = tempDate
            })

            getData = response.data;
            newchart.data = getData;
            console.log(getData)
        });
}

function GetTilstand(){
    let tilstandParag: HTMLParagraphElement = <HTMLParagraphElement>document.getElementById("tilstand");
    
    axios.get<Status>(tilstandUrl).then((response: AxiosResponse<Status>) => {
        if(response.data.allowChange){
            tilstandParag.innerHTML = "Automatisk";
            tilstandParag.className = "on"
        } 
        else {
            tilstandParag.innerHTML = "Manuel";
            tilstandParag.className = "off";
        }
    })
}