import CallApi from "../../../utils/callApi";
import DialogWrapper from "../../shared/dialog-wrapper"

const FormEditCopy = ({ onExit, copy, refresh }) => { 
    const Statuses = ["Còn sách", "Đang mượn", "Sách hỏng", "Sách mất"];

    const onConfirm = async (action) => {
        try {
            switch (action) {
                case 1:
                    await CallApi.post('/lbooks', {  })
                    break;
                case 2:
                    await CallApi.put('/categories/' + copy.lBookID, {
                        params: {
                            
                        }
                    });
                    break;
                case 3:
                    await CallApi.delete('/lbooks/' + copy.lBookID);
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

    return <DialogWrapper onClickOut={onExit}>
        <div className="edit-copy">
            <div className="title">Chỉnh sửa cuốn sách #{copy.lBookID}</div>
            <div className="title">(mới xóa được thôi)</div>
            <div>
                <label htmlFor="status">Tình trạng:</label>
                <select id="status">
                    {Statuses.map(x => <option key={x} value={x}>
                        {x}
                    </option>)}
                </select>
            </div>
            <div>
                <label htmlFor="note">Ghi chú:</label>
                <input type="text" id="note" />
            </div>
            <div>
                <div className="btn" onClick={onExit}>Hủy</div>
                <div className="btn" onClick={() => onConfirm(3)}>Xóa</div>
                <div className="btn">Cập nhật</div>
            </div>
        </div>
    </DialogWrapper>
}

export default FormEditCopy