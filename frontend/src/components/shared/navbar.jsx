import { useState } from "react";
import { Link } from "react-router-dom";
import Login from "./login-frm";
import useGlobalContext from '../../contexts/GlobalContext'
import cloud1 from '../../resources/imgs/cloud_1.png';
import cloud2 from '../../resources/imgs/cloud_2.png';
import sun from '../../resources/imgs/sun.png';
import moon from '../../resources/imgs/moon.png';
import stars from '../../resources/imgs/stars.png';
import logo from '../../resources/imgs/logo.png';

const Nav = () => { 
    const [login, setLogin] = useState(false);
    const { lightMode, toggleLightMode, token, removeToken, role } = useGlobalContext();

    return <>
        <div className={`navbar ${!lightMode ? 'night': ''}`}>
            <div className="left">
                <h2><img src={logo} alt="" /></h2>
            </div>
            <div className="middle">
                <Link to={'/BLibrary'}>Trang chủ</Link>
                <Link to={'/BLibrary/About'}>Giới thiệu</Link>
                <Link to={'/BLibrary/Category'}>Thể loại</Link>
                <Link to={'/BLibrary/Book'}>Sách</Link>
                {token &&  role === 'Admin' && <Link to={'/BLibrary/Staff'}>Thủ thư</Link>}
            </div>
            <div className="right">
                <div className="flex-fill"></div>
                {token ?
                    <div className="btn pill" onClick={removeToken}>Đăng Xuất</div>
                    :
                    <div className="btn pill" onClick={() => { setLogin(true) }}>Đăng nhập</div>
                }
                
                <div className={`light-mode-toggle ${!lightMode && 'night'}`} onClick={toggleLightMode}>
                    <div className="bg abs-fill"></div>
                    <div className="bg-night abs-fill"></div>
                    <img src={cloud1} alt="" className="cloud-1" />
                    <img src={cloud2} alt="" className="cloud-2" />
                    <div className="sun-wrap">
                        <img src={sun} alt="" className="sun" />
                        <img src={moon} alt="" className="moon" />
                        <div className="moon-hole"></div>
                    </div>
                    <img src={stars} alt="" className="stars" />
                </div>
            </div>

            {login && <Login setLogin={setLogin} />}
        </div>
    </>
}

export default Nav;