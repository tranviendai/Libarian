import CallApi from "../../../utils/callApi";
import DialogWrapper from "../../shared/dialog-wrapper"

const PayFineDialog = ({ item, onExit, refresh }) => { 
    
    const onConfirm = async () => { 
        try {
            await CallApi.delete('/PenaltyTickets/' + item.callCardID);
            alert('Đã trả');
            refresh();
            onExit();
        } catch (err) { 
            console.log(err);
        }
    }

    return <DialogWrapper onClickOut={onExit}>
        <div className="p-5" style={{backgroundColor: 'white'}}>
            <p>Xác nhận trả phạt cho phiếu #{item.callCardID}?</p>
            <p><b>Số tiền: {item.price?.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}đ</b></p>

            <div className="m-row mt-4">
                <div className="flex-fill"></div>
                <div>
                    <div className="btn btn-primary me-3" onClick={onConfirm}>Xác nhận</div>
                    <div className="btn btn-secondary" onClick={onExit}>Hủy</div>
                </div>
            </div>
        </div>
    </DialogWrapper>
}

export default PayFineDialog