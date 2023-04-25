import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import CallApi from "../utils/callApi";

const LibCardDetail = () => { 
    const { id } = useParams();
    const [detail, setDetail] = useState({});
    const [history, setHistory] = useState([]);

    useEffect(() => { 

        const fetchApi = async () => { 
            const resp = await CallApi('librarycards/' + id);
            const data = resp.data;

            setDetail(data.detail);
            setHistory(data.history);
        }

        fetchApi();
    }, [id])

    return <div className="libcard-detail-page">
        <div className="header">Thông tin thẻ: </div>
        <div className="detail">
            <div>mã: {detail.libraryCardID}</div>
            <div className="name">tên: {detail.fullName}</div>
            <div>ngày cấp: {detail.startDate}</div>
            <div>hạn: {detail.expirationDate}</div>
        </div>
        <div className="header">Lịch sử mượn: </div>
        <div className="history">
            {history.map((x, ind) => <div className="item" key={ind}>
                {ind+1}/ mã: {x.lBookID}, ngày mượn: {x.startDate}, hạn: {x.deadline}, ngày trả: {x.endDate}, tình trạng sách: {x.bookStatus}
            </div>)} 
        </div>
    </div>
}

export default LibCardDetail;