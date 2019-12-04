import axios, {
    AxiosResponse,
    AxiosError
} from "../../node_modules/axios/index"; // Don't worry

var MyStorage = window.localStorage;

//GetAll


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
    'Forkert din bøv'
 , 'Try again', 'Come on now bro',
 'You suck at logging in', 'Try again..', 'Login failed',
  'Are you really trying to login?', 'Houston we have a problem',
   'Username or password is incorrenct'
 ]
var randomError = listError[Math.floor(Math.random() * listError.length)];





if(loginBruger != null && loginKodeord != null){

    try {
        if(admindUser == loginBruger && AdminPass == loginKodeord)
        {

            MyStorage.setItem('logged', 'true')
            logOutput.innerHTML = "Success";
            location.href = "mainsite.htm";
            console.log(logOutput.innerHTML)
        }
        else    {
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
            console.log(logOutput.innerHTML)
            logOutput.innerHTML = randomError;
            bruger.value = "";
            kodeord.value = "";
        }
    }
}


}
