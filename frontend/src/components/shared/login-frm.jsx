import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faEnvelope, faLock } from "@fortawesome/free-solid-svg-icons";
import BGA from '../../resources/imgs/form-bg-1.png'
import BGB from '../../resources/imgs/form-bg-2.png'
import DialogWrapper from "./dialog-wrapper";

library.add(faEnvelope);
library.add(faLock);

const LoginImg = require('../../resources/imgs/login.png');

const Login = ({ setLogin }) => { 
    
    const Exit = () => { 
        setLogin(false);
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
                        <input type="text" placeholder='Tên người dùng' autoFocus={true} />
                        <div className="icon">
                            <FontAwesomeIcon icon="fa-solid fa-envelope" />
                        </div>
                    </div>
                    <div className="input-wrap">
                        <input type="text" placeholder='Mật khẩu' />
                        <div className="icon">
                            <FontAwesomeIcon icon="fa-solid fa-lock" />
                        </div>
                    </div>

                    <label>
                        <input type="checkbox" />
                        Lưu mật khẩu
                    </label>

                </div>
                <div className="frm-2" style={{ backgroundImage: `url(${BGB})` }}>
                    <p className="btn">Quên mật khẩu?</p>
                    <div className="btns">
                        <div className="btn pill" onClick={Exit}>Thoát</div>
                        <div className="btn pill confirm">Đăng nhập</div>
                    </div>
                </div>
            </form>
        </div>
    </DialogWrapper>
}

export default Login;