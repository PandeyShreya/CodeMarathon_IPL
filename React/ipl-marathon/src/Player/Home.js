import pic1 from "../Image/pic1.jpg"
import Navbar from "./Navbar"

const Home=()=>{
    return <>  
    <Navbar/>
    <img src={pic1} style={{ width: "70%"}} alt='Pic Image' />   
    </>
}

export default Home



