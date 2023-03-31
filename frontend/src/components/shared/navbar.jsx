import { useState } from "react";
import { Link } from "react-router-dom";
import Login from "./login-frm";
import useGlobalContext from '../../contexts/GlobalContext'
import cloud1 from '../../resources/imgs/cloud_1.png';
import cloud2 from '../../resources/imgs/cloud_2.png';
import sun from '../../resources/imgs/sun.png';
import moon from '../../resources/imgs/moon.png';
import stars from '../../resources/imgs/stars.png';

const Nav = () => { 
    const [login, setLogin] = useState(false);
    const { lightMode, toggleLightMode } = useGlobalContext();

    return <div className={`navbar`}>
        <div className="left">
            <h2>Thư viện Thầy Ái</h2>
            <Link to={'/LMS'}>Trang chủ</Link>
            <Link to={'/LMS/Category'}>Thể loại</Link>
            <Link to={'/LMS/Book'}>Tìm sách</Link>
        </div>
        <div className="right">
            <div className="btn pill" onClick={() => { setLogin(true) }}>Đăng nhập</div>
            <div className={`light-mode-toggle ${!lightMode && 'night'}`} onClick={toggleLightMode}>
                <div className="bg abs-fill"></div>
                <div className="bg-night abs-fill"></div>
                <img src={cloud1} alt="" className="cloud-1"/>
                <img src={cloud2} alt="" className="cloud-2" />
                <div className="sun-wrap">
                    <img src={sun} alt="" className="sun"/>
                    <img src={moon} alt="" className="moon" />
                    <div className="moon-hole"></div>
                </div>
                <img src={stars} alt="" className="stars" />
            </div>
        </div>

        {login && <Login setLogin={setLogin}/>}
    </div>
}

export default Nav;