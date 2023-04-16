import { useState } from "react";
import DialogWrapper from "../../shared/dialog-wrapper"
import {CallApiWithToken} from "../../../utils/callApi";
import useGlobalContext from "../../../contexts/GlobalContext";
import ConfirmDialog from "../../shared/dialog-confirm";

const FrmAddCate = ({setShow, item, setRefresh}) => { 
    const {token} = useGlobalContext();
    
    const [name, setName] = useState((item && item.nameCategory) || '');
    const [showConfirm, setShowConfirm] = useState(false);

    const Exit = () => { 
        setShow(false);
    }

    const refresh = () => { 
        setRefresh(x => !x);
    }

    //action = 1: add, 2: update, 3: delete
    const onConfirm = async (action) => {

        try {
            switch (action) { 
                case 1:
                    if(name) await CallApiWithToken(token).post('/categories', { nameCategory: name })
                    break;
                case 2:
                    if (name && item && item.categoryID) await CallApiWithToken(token).put('/categories/' + item.categoryID, {
                        categoryID: item.categoryID,
                        nameCategory: name
                    });
                    break;
                case 3:
                    if (item && item.categoryID) await CallApiWithToken(token).delete('/categories/' + item.categoryID);
                    break;
                default: return;

            }
            alert('Thành công!');
            refresh();
            Exit();
        } catch (err) {
            alert('Có lỗi xảy ra')
            console.log('failed: ', err)
        }
    }

    const keyDown = (e) => { 
        if (e.key === 'Enter') { 
            if (item && item.categoryID) onConfirm(2);
            else onConfirm(1);
            return;
        }
    }

    return <DialogWrapper onClickOut={Exit}>
        <div className="add-dialog">
            <h2>
                {item && item.categoryID ? "Cập nhật" : "Thêm"} thể loại
            </h2>
            <div>
                <input type='text' placeholder="Tên thể loại" autoFocus={true} value={name}
                    onKeyDown={(e) => { keyDown(e) }} onChange={(e) => { setName(e.target.value) }} />
                {(!item || !item.categoryID) && <span className="btn pill btn-add" onClick={() => { onConfirm(1) }}>Thêm</span>}
            </div>
            <div className="btn-cancle" onClick={Exit}>x</div>
            <div className="btns">
                {item && item.categoryID &&
                    <>
                        <span className="btn pill delete" onClick={() => { onConfirm(3) }}>Xóa</span>
                        <span className="btn pill" onClick={() => { onConfirm(2) }}>Cập Nhật</span>
                    </>
                }
            </div>
            
            {showConfirm &&
                <DialogWrapper noBG onClickOut={() => { setShowConfirm(false) }}>
                    <ConfirmDialog msg={'Có chắc muốn xóa không?'} />
                </DialogWrapper>
            }

            {/* <div className="toast update">
                <img src="" alt="" />
                Cập nhật thành công!
            </div>
            <div className="toast delete">
                <img src="" alt="" />
                <span>Đã xóa thành công !!</span>
            </div> */}
        </div>
    </DialogWrapper>
}

export default FrmAddCate;

//  <label>
//                 {(item && item.categoryID) ?<span>Cập nhật</span> : <span>Thêm</span>} thể loại: <br />
//                 <input type='text' placeholder="tên thể loại" autoFocus={true}
//                     value={name} onKeyDown={(e) => { keyDown(e) }} onChange={(e) => {setName(e.target.value)}} />
//             </label>
//             <div className="btns">
//                 <div className="btn pill cancel" onClick={Exit}>Hủy</div>
//                 {
//                     // eslint-disable-next-line
//                     (item && item.categoryID != null) ? <>
//                         <div className="btn pill delete" onClick={() => { onConfirm(3) }}>Xóa</div>
//                         <div className="btn pill" onClick={() => { onConfirm(2) }}>Cập Nhật</div>
//                     </> : <div className="btn pill" onClick={() => { onConfirm(1) }}>Thêm</div>
//                 }
//             </div>