import axios, {
    AxiosResponse,
    AxiosError
} from "../../node_modules/axios/index"; // Don't worry

var MyStorage = window.localStorage;

interface Logging {
    id: number;
    dato: string;
    luftfugtighed: number;
    aktiv: boolean;
}

//henter den nyeste entry fra databasen
let urlgetLast : string = "https://autovenrest.azurewebsites.net/api/Logging/getLast";

let humidInside : HTMLDivElement = <HTMLDivElement>document.getElementById("humidInside")

//Humidtiy
let humidOutput : HTMLDivElement = <HTMLDivElement>document.getElementById("humidOutput")


let urlHumid: string = "http://api.openweathermap.org/data/2.5/weather?q=Roskilde,DK&APPID=f0f18be3bdaa24b17b922c92f5cd9279"

//Login

let loginBut : HTMLButtonElement = <HTMLButtonElement>document.getElementById("buttonLogin");
if(loginBut != null)
{
    loginBut.addEventListener("click", loginFuc)
}

let logUDBut : HTMLButtonElement = <HTMLButtonElement>document.getElementById("buttonLogud")
if(logUDBut != null)
{
    logUDBut.addEventListener("click", logudFunc)
}

let bruger : HTMLInputElement = <HTMLInputElement>document.getElementById("email");
let kodeord : HTMLInputElement = <HTMLInputElement>document.getElementById("kodeord");
let logOutput : HTMLDivElement = <HTMLDivElement>document.getElementById("loginValidation");



function getHumid(): void{
    axios.get(urlHumid)
    .then((main: AxiosResponse) => {
        let data = main.data["main"]["humidity"];
        let longHtml: string = "<p>";
      
        console.log(data)

        longHtml +=  "Fugtighed udenfor: " + data + "%"; 
        
        longHtml += "</p>";
        humidOutput.innerHTML = longHtml;
    })
    .catch((error: AxiosError) => {
        console.log(error);
        humidOutput.innerHTML = error.message;
    });
}

function getLatestLog() : void {
   

    axios.get<Logging>(urlgetLast)
        .then((response: AxiosResponse<Logging>) =>{
        let dataOne : Logging = response.data;
        let longHtml2: string = "<p>";
    
        console.log(dataOne);
        humidInside.innerHTML = JSON.stringify(dataOne);

        console.log(dataOne.id, dataOne.dato, dataOne.aktiv, dataOne.luftfugtighed);
        
        longHtml2 += "Fugtighed indenfor: " + dataOne.luftfugtighed.toPrecision(2) + "%";
               
          
        longHtml2 += "</p>";
        humidInside.innerHTML = longHtml2; 
    });
         
       
   }

function logudFunc(): void{

    MyStorage.removeItem('logged');
    location.href = "index.htm";
}

function loginFuc(): void{
let loginBruger = bruger.value;
let loginKodeord = kodeord.value;
let admindUser : String = "Admin";
let AdminPass : String = "Admin";

let listError : string[] = [
    'Forkert din bøv', 'Try again', 'Come on now bro',
 'You suck at logging in', 'Try again..', 'Login failed',
  'Are you really trying to login?', 'Houston we have a problem',
   'Username or password is incorrect'
 ]
var randomError = listError[Math.floor(Math.random() * listError.length)];

if(loginBruger != null && loginKodeord != null){

    try {
        if(admindUser == loginBruger && AdminPass == loginKodeord)
        {
            MyStorage.setItem('logged', 'true')
            logOutput.innerHTML = "Success";
            location.href = "mainsite.htm";
        }
        else {
          logOutput.innerHTML = randomError;
        }
    }
    catch(e) {
        if(e instanceof Error)
        {
            logOutput.innerHTML = "Something went wrong: " + e.message;
        }
        if(function (error: AxiosError): void {
            logOutput.innerHTML = error.message;
        })
        throw e;
    }
    finally{

        if(admindUser != loginBruger && AdminPass != loginKodeord)
        {
            logOutput.innerHTML = randomError;
            bruger.value = "";
            kodeord.value = "";
        }
    }
}
}

function timer(): void{
    let b = true;
    setInterval(function(){
        let t = new Date().getMinutes();
        if(t == 1 || t == 16 || t == 31 || t == 46){
            if(b){
                //Kode til at tjekke status og stoppe/starte blæserbladet her
                

                console.log(t);
                b = false;
            }
        }else{
            if(!b) b = true;
        }
    },1000)
}

//Kører kun på main siden, metoder der altid kører på main
if(window.location.pathname == "/mainsite.htm"){
    timer();
    getHumid();
    getLatestLog();
}