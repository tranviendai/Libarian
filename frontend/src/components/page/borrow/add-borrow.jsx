import { useState } from "react";
import DialogWrapper from "../../shared/dialog-wrapper";
import { CallApiWithToken } from "../../../utils/callApi";
import useGlobalContext from "../../../contexts/GlobalContext";
import getErrorMsg from "../../../utils/getErrorMsg";

const AddBorrowDialog = ({ onExit, refresh }) => { 
    
    const [cardId, setCardId] = useState('');
    const [copyId, setCopyId] = useState('');
    const [error, setError] = useState('');

    const {token} = useGlobalContext();

    const onSubmit = async () => {
        try {
            await CallApiWithToken(token).post('/CallCards', {
                lBookID: copyId,
                libraryCardID: cardId,
                bookStatus: '',
                callCardID: ''
            })
            alert('Mượn thành công');
            onExit();
            refresh();
        } catch (err) { 
            console.log(err);
            if (err.response.status === 404) { 
                setError('Không tìm thấy thông tin trùng khớp');    
                return;
            }
            let msg = getErrorMsg(err);
            setError(msg);
        }
     }

    return <DialogWrapper onClickOut={onExit}>
        <div className="col-4 p-5" style={{ backgroundColor: 'white' }}>
            <h3 className="mb-3">Mượn sách</h3>
            <div className="form-floating mb-3">
                <input type="text" className="form-control" id="card" placeholder=" " value={cardId} onChange={(e)=> setCardId(e.target.value)} />
                    <label for="card">Mã thẻ</label>
            </div>
            <div className="form-floating mb-3">
                <input type="text" className="form-control" id="book" placeholder=" " value={copyId} onChange={(e) => setCopyId(e.target.value)} />
                <label for="book">Mã cuốn sách</label>
            </div>
            
            {error &&
                <p className="text-danger"><i>{error}</i></p>
            }
            <button className="btn btn-primary" onClick={onSubmit}>Xác nhận</button>
        </div>
    </DialogWrapper>
}

export default AddBorrowDialog;