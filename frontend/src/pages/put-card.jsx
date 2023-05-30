import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { CallApiWithToken } from '../utils/callApi'
import {convertToDMY, convertToYMD} from '../utils/convertDate'
import useGlobalContext from '../contexts/GlobalContext'
import useFetch from "../utils/useFetch";
import getErrorMsg from "../utils/getErrorMsg";

const PutCardPage = () => {
    const { id } = useParams();
    const { token } = useGlobalContext();
    const navigate = useNavigate();

    const initialFrm = {
        libraryCardID: id||'H1000' , fullname: '', cardStatus: 'Yes'
    };
    const [frmData, setFrmData] = useState(initialFrm);
    const [error, setError] = useState('');

    const { data: card } = useFetch('/LibraryCards/' + id);
    useEffect(() => {
        if (card) setFrmData({ ...card.detail, expirationDate: convertToYMD(card.detail.expirationDate) });
    }, [card])

    const onInputChange = (e) => {
        const { id, value } = e.target;
        setFrmData(x => { return { ...x, [id]: value } });
    }

    const onFrmSubmit = async (e) => { 
        e.preventDefault();
        try {
            if (id) { 
                frmData.expirationDate = convertToDMY(frmData.expirationDate); 
                await CallApiWithToken(token).put('/LibraryCards/' + id, frmData)
            }
            else await CallApiWithToken(token).post('/LibraryCards', frmData);

            alert(id ? 'Đã chỉnh sửa' : 'Đã thêm');
            if (id) navigate('/BLibrary/LibCard/' + id);
            setFrmData(initialFrm);
        } catch (err) { 
            let msg = getErrorMsg(err);
            setError(msg);
            console.log(err);
        }
    }

    return <div className="container col-6">
        <div className="page-title">
            {id ? 'Cập nhật thẻ #' + id : 'Thêm thẻ thư viện'}
        </div>

        <form onSubmit={(e) => onFrmSubmit(e)}>
            <div className="row mb-3">
                {id==null &&
                    <div className="form-floating col-12">
                    <input type="text" id="fullname" className="form-control" placeholder=" "
                        value={frmData['fullname']} onChange={onInputChange}/>
                    <label htmlFor="fullname" className="ps-4">Họ tên: </label>
                </div>
                }
                {/* <div className="form-floating col-4">
                    <input type="date" id="birth_date" name="birth_date" className="form-control" placeholder=" "
                        value={frmData['birth_date']} onChange={onInputChange}/>
                    <label htmlFor="birth_date" className="ps-4">Ngày sinh:</label>
                </div> */}
            </div>

            {id &&<div className="row mb-3">
                <div className="col-8"></div>
                <div className="form-floating col-4">
                    <input type="date" id="expirationDate" className="form-control" placeholder=" "
                        value={frmData['expirationDate']} onChange={onInputChange} />
                    <label htmlFor="expirationDate" className="ps-4">Hạn</label>
                </div>
            </div> }

            {error && 
                <p className="text-danger"><i>{error}</i></p>
            }
            <button className="btn btn-primary" style={{float: 'right'}}>Xác nhận</button>
        </form>

    </div>
}

export default PutCardPage;