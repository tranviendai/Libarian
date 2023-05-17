import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import CallApi from "../utils/callApi";
import useFetch from "../utils/useFetch";

const PutStaffPage = () => { 
    const { id } = useParams();
    const navigate = useNavigate();

    const initForm = { fullName: '', phone: '', email: '', sex: '', address: '' ,Username: '', Password: '', active: true };
    const [form, setForm] = useState(initForm);
    const [error, setError] = useState('');

    const { data: emp } = useFetch('/emp/detail/' + (id || -1));

    useEffect(() => { 
        if (emp) setForm(emp);
    }, [emp])

    const onFormChange = (e) => {
        const { id, value } = e.target;
        setForm(x => { return { ...x, [id]: value } })
    }

    const onFrmSubmit = async (e) => {
        e.preventDefault();
        try {
            if (!id) {
                await CallApi.post('/Auth/Register', form);
                alert('Đã tạo')
                setForm(initForm);
                setError('');
            } else { 
                await CallApi.post('/emp/update/' + id, form);
                navigate('/BLibrary/Staff')
            }

        } catch (err) {
            console.log(err);
            setError(err.response.data.msg)
        }
    }

    const block = async () => { 
        try {
            await CallApi.post('/account/block/' + form?.acc_id);
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

                <div className={`form-floating col-3`}>
                    <input type="text" id="sex" className="form-control" placeholder=" "
                        value={form?.sex} onChange={onFormChange} />
                    <label htmlFor="sex" className="ps-4">Giới tính:</label>
                </div>
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
                        <input type="text" id="Username" className="form-control" placeholder=" "
                            value={form?.Username} onChange={onFormChange}/>
                        <label htmlFor="Username" className="ps-4">Tài khoản:</label>
                    </div>
                    <div className="form-floating col-6">
                        <input type="password" id="Password" className="form-control" placeholder=" "
                            value={form?.Password} onChange={onFormChange}/>
                        <label htmlFor="Password" className="ps-4">Mật khẩu:</label>
                    </div>
                </div>
            }
            
            <div className="m-row">
                {error && <p className="text-danger">{error}</p>}
                <div className="flex-fill"></div>
                <div>
                    {id && <button className={`btn me-4 ${form?.active ? 'btn-danger' : 'btn-success'}`} onClick={block}>
                        {form?.active ? 'Khóa' : 'Mở khóa'}
                    </button>}
                    <button className="btn btn-primary">{id ? 'Chỉnh sửa' : 'Thêm'}</button>
                </div>
            </div>
        </form>

    </div>
}

export default PutStaffPage;