import MountainBG from '../resources/imgs/mountain-bg.png';
import AboutBG from '../resources/imgs/about-bg.png';
import { CallApiWithToken } from '../utils/callApi';


const AboutPage = () => { 
    const test = CallApiWithToken('token');
    test.get('/books');

    return <div className="about-page-2">
        <img src={MountainBG} alt="" className='mountain-bg' />
        <div className="left">
            <div className="title">
                Chào Mừng Bạn Đến Với Thư Viện B HUFLIT
            </div>
            <p>Thư viện B của Trường Đại học Ngoại ngữ – Tin học Thành phố Hồ Chí Minh - HUFLIT - được thành lập cùng với sự ra đời của trường. Thư viện có nhiệm vụ tham mưu cho Hiệu trưởng công tác quản lý thư viện; tổ chức thu thập, lưu trữ, bảo quản và khai thác tốt nguồn tài liệu phục vụ cho việc giảng dạy, nghiên cứu học tập của cán bộ, giảng viên, nhân viên và sinh viên của trường.</p>
        </div>
        <div className="right"><img src={AboutBG} alt="" className='about-bg' /></div>
    </div>
}

export default AboutPage;