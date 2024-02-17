import { Outlet } from "react-router-dom";
import Header from "../components/Header";

const Layout = () => {
    return (
        <main className="App" >
            <Header />
            <div className='main-container'>
                <Outlet />
            </div>
        </main>
    )
}

export default Layout