import { useState } from "react";
import {CallApiWithToken} from "../../../utils/callApi";
import DialogWrapper from "../../shared/dialog-wrapper"
import useGlobalContext from "../../../contexts/GlobalContext";

const FormEditCopy = ({ onExit, copy, refresh }) => { 
    const Statuses = ["Còn sách", "Đang mượn", "Sách hỏng", "Sách mất"];
    const [formData, setFormData] = useState({ lBookID: copy.lBookID, status: copy.status, note: copy.note, bookID: copy.bookID });

    const {token} = useGlobalContext();

    const onConfirm = async (action) => {
        try {
            switch (action) {
                case 1:
                    await CallApiWithToken(token).post('/lbooks')
                    break;
                case 2:
                    await CallApiWithToken(token).put('/lbooks/' + copy.lBookID, {
                        ...formData
                    });
                    break;
                case 3:
                    await CallApiWithToken(token).delete('/lbooks/' + copy.lBookID);
                    break;
                default: return;

            }
            alert('Thành công!');
            refresh(x => !x)
            onExit();
        } catch (err) {
            alert('Có lỗi xảy ra')
            console.log('failed: ', err)
        }
    }

    const onFormChange = (e) => { 
        const { id, value } = e.target;
        setFormData(x => { return {...x, [id]: value} })
    }

    return <DialogWrapper onClickOut={onExit}>
        <div className="edit-copy">
            <div className="title">Chỉnh sửa cuốn sách #{copy.lBookID}</div>
            <div>
                <label htmlFor="status">Tình trạng:</label>
                <select id="status" value={formData.status || Statuses[0]} onChange={(e) => onFormChange(e)}>
                    {Statuses.map(x => <option key={x} value={x}>
                        {x}
                    </option>)}
                </select>
            </div>
            <div>
                <label htmlFor="note">Ghi chú:</label>
                <input type="text" id="note" value={formData.note} onChange={(e) => onFormChange(e)} />
            </div>
            <div>
                <div className="btn" onClick={onExit}>Hủy</div>
                <div className="btn" onClick={() => onConfirm(3)}>Xóa</div>
                <div className="btn" onClick={() => onConfirm(2)}>Cập nhật</div>
            </div>
        </div>
    </DialogWrapper>
}

export default FormEditCopy