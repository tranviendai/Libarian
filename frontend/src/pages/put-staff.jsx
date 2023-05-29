import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import CallApi, { CallApiWithToken } from "../utils/callApi";
import useFetch from "../utils/useFetch";
import { convertToDMY, convertToYMD } from "../utils/convertDate";
import useGlobalContext from "../contexts/GlobalContext";

const PutStaffPage = () => { 
    const { id } = useParams();
    const { token } = useGlobalContext();
    const navigate = useNavigate();

    const initForm = { UserName: '', sex: '', address: '', fullName: '', Password: '' ,birthday: '', email: ''};
    const [form, setForm] = useState(initForm);
    const [error, setError] = useState('');

    const { data: emp } = useFetch('/Staffs/' + (id || -1), null, token);

    useEffect(() => { 
        if (emp) setForm({...emp, birthday: convertToYMD(emp.birthday)});
        console.log(emp)
    }, [emp])

    const onFormChange = (e) => {
        const { id, value } = e.target;
        setForm(x => { return { ...x, [id]: value } })
    }

    const onFrmSubmit = async (e) => {
        e.preventDefault();
        try {
            const body = {...form,
                    birthday: convertToDMY(form.birthday)
                }
            if (!id) { //Thêm
                await CallApi.post('/Auth/Register', body);
                alert('Đã tạo')
                setForm(initForm);
                setError('');
            }
            if (id){
                await CallApiWithToken(token).put('/Staffs/' + id, body);
            }
 
            navigate('/BLibrary/Staff')

        } catch (err) {
            console.log(err);
            setError(err.response.data.msg)
        }
    }

    const block = async () => { 
        try {
            await CallApiWithToken(token).delete('/Staffs/' + id);
            navigate('/BLibrary/Staff')
        } catch (err) {
            alert('Có lỗi');
            console.log(err);
        }
    }

    return <div className="container col-6">
        {id ? <div className="page-title"> Cập nhật thủ thư</div> :
            <div className="page-title">Thêm thủ thư</div>
        }

        <form onSubmit={(e) => onFrmSubmit(e)}>
            <div className="row mb-3">
                <div className={`form-floating col-9`}>
                    <input type="text" id="fullName" className="form-control" placeholder=" "
                        value={form?.fullName} onChange={onFormChange}/>
                    <label htmlFor="fullName" className="ps-4">Họ tên: </label>
                </div>

                {/* <div className={`form-floating col-3`}>
                    <input type="text" id="sex" className="form-control" placeholder=" "
                        value={form?.sex} onChange={onFormChange} />
                    <label htmlFor="sex" className="ps-4">Giới tính:</label>
                </div> */}
            </div>

            <div className="row mb-3">
                <div className={`form-floating col-6`}>
                    <input type="text" id="email" className="form-control" placeholder=" "
                        value={form?.email} onChange={onFormChange}/>
                    <label htmlFor="email" className="ps-4">Email:</label>
                </div>
                <div className="form-floating col-6">
                    <input type="text" id="address" className="form-control" placeholder=" "
                        value={form?.address} onChange={onFormChange} />
                    <label htmlFor="address" className="ps-4">Địa chỉ:</label>
                </div>
            </div>

            {!id &&
                <div className="row mb-3">
                    <div className={`form-floating col-6`}>
                        <input type="text" id="UserName" className="form-control" placeholder=" "
                            value={form?.UserName} onChange={onFormChange}/>
                        <label htmlFor="UserName" className="ps-4">Tài khoản:</label>
                    </div>
                    <div className="form-floating col-6">
                        <input type="password" id="Password" className="form-control" placeholder=" "
                            value={form?.Password} onChange={onFormChange}/>
                        <label htmlFor="Password" className="ps-4">Mật khẩu:</label>
                    </div>
                </div>
            }
            <div>
            <label htmlFor="birthday" className="ps-4">Ngày sinh:</label>
            <input type="date" id="birthday" className="form-control" placeholder=" "
                    onChange={onFormChange} value={form && form['birthday']} />
            </div>
            
            <div className="m-row">
                {error && <p className="text-danger">{error}</p>}
                <div className="flex-fill"></div>
                <div>
                    <button className="btn btn-primary">{id ? 'Chỉnh sửa' : 'Thêm'}</button>
                </div>
            </div>
        </form>

        <div> 
            {id && 
            <button className={`btn me-4 ${form.sex == "Yes" ? 'btn-danger' : 'btn-success'}`} onClick={block}>
                {form.sex== "Yes"? 'Khóa' : 'Mở khóa'}
            </button>}
        </div>

    </div>
}

export default PutStaffPage;