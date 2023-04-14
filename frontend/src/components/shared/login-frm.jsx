import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faEnvelope, faLock } from "@fortawesome/free-solid-svg-icons";
import BGA from '../../resources/imgs/form-bg-1.png'
import BGB from '../../resources/imgs/form-bg-2.png'
import DialogWrapper from "./dialog-wrapper";
import { useState } from "react";
import CallApi from "../../utils/callApi";
import useGlobalContext from "../../contexts/GlobalContext";

library.add(faEnvelope);
library.add(faLock);

const LoginImg = require('../../resources/imgs/login.png');

const Login = ({ setLogin }) => { 
    const { addToken, changeRole } = useGlobalContext();
    
    const [Username, setUsername] = useState('');
    const [Password, setPassword] = useState('');

    const Exit = () => { 
        setLogin(false);
    }

    const submitLogin = async () => { 
        try {
            const resp = await CallApi.post('/Auth/Login', {
                Username,
                Password
            });
            const { token, role } = resp.data;
            addToken(token);
            changeRole(role);

            setLogin(false);
        } catch { 
            alert('login failed');
        }
    }

    return <DialogWrapper onClickOut={Exit}>
        <div className="login-frm">
            <div className="img-wrap">
                <img src={LoginImg} alt="" />
            </div>

            <form>
                <div className="frm-1" style={{ backgroundImage: `url(${BGA})`}}>
                    <div className="title text-center">Chào mừng bạn</div>
                    <p className='text-center'>đến với thư viện HUFLIT</p>
                    <div className="input-wrap">
                        <input type="text" placeholder='Tên người dùng' autoFocus={true} onChange={e => setUsername(e.target.value)} value={Username} />
                        <div className="icon">
                            <FontAwesomeIcon icon="fa-solid fa-envelope" />
                        </div>
                    </div>
                    <div className="input-wrap">
                        <input type="password" placeholder='Mật khẩu' onChange={e => setPassword(e.target.value)} value={Password} />
                        <div className="icon">
                            <FontAwesomeIcon icon="fa-solid fa-lock" />
                        </div>
                    </div>

                    <label style={{ visibility: 'hidden' }} >
                        <input type="checkbox" />
                        Lưu mật khẩu
                    </label>

                </div>
                <div className="frm-2" style={{ backgroundImage: `url(${BGB})` }}>
                    <p className="btn" style={{ visibility: 'hidden' }}>Quên mật khẩu?</p>
                    <div className="btns">
                        <div className="btn pill" onClick={Exit}>Thoát</div>
                        <div className="btn pill confirm" onClick={submitLogin}>Đăng nhập</div>
                    </div>
                </div>
            </form>
        </div>
    </DialogWrapper>
}

export default Login;