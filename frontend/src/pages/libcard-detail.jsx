import { Link, useParams } from "react-router-dom";
import useFetch from "../utils/useFetch";
import DataTable from "../components/shared/data-table";
import { CallApiWithToken } from "../utils/callApi";
import useGlobalContext from "../contexts/GlobalContext";
import { useEffect, useState } from "react";

const LibCardDetail = () => {
    const { id } = useParams();

    const { token } = useGlobalContext();

    const [card, setCard] = useState({});
    const [history, setHistory] = useState([]);

    const { data: cardData, loading } = useFetch('/librarycards/' + id);
    useEffect(() => { 
        if (!cardData) return;
        setCard(cardData.detail);
        setHistory(cardData.history);
    }, [cardData])

    const headers = ['Mã phiếu', 'Sách', 'mã sách', 'Ngày mượn', 'Hạn trả', 'Ngày trả', 'Tình trạng sách'];
    const rows = history?.map(x => {
        return {
            onRowSelected: () => { },
            rowData: ['#' + x.callCardID, x.title, '#' + x.lBookID, x.startDate, x.deadline,
                x.endDate || 'Chưa trả', x.endDate ? x.bookStatus : 'Chưa trả']
        }
    })

    const blockCard = async () => {
        try {
            await CallApiWithToken(token).post('/card/block/' + id);
            window.location.reload();
        } catch {
            alert('Failed')
        }
    }

    return <div className="container-80">
        <div className="page-title">Thông tin thẻ</div>

        {loading && <div className="loader"></div>}
        {card && <div className="mx-auto col-8">
            <h3>Mã thẻ: #{card.libraryCardID}</h3>
            <div className="input-group mb-3">
                <span className="input-group-text">Tên</span>
                <div className="form-control">{card.fullName}</div>
                <span className="input-group-text">tình trạng</span>
                <div className="form-control">{card.cardStatus === 'Yes' ? 'Hoạt động' : 'Tạm khóa'}</div>
            </div>
            
            <div className="input-group mb-3">
                <span className="input-group-text">Ngày mở</span>
                <div className="form-control">{card.startDate}</div>
                <span className="input-group-text">Hạn</span>
                <div className="form-control">{card.expirationDate}</div>
            </div>

            {token &&
                <>
                    <Link to={'/BLibrary/UpdateCard/' + card.libraryCardID}>
                        <div className="btn btn-primary me-4">Chỉnh sửa</div>
                    </Link>
                    <div className={card.cardStatus === 'Yes' ? 'btn btn-danger' : 'btn btn-success'} onClick={blockCard}>
                        {card.cardStatus === 'Yes' ? 'Khóa thẻ' : 'Mở khóa thẻ'}
                    </div>
                </>
            }
        </div>}

        <div className="page-title">Lịch sử mượn</div>

        {history ? <DataTable headers={headers} rows={rows} /> : <h3>Không có lịch sử mượn</h3>}
    </div>
}

export default LibCardDetail;

// import { useEffect, useState } from "react";
// import { useParams } from "react-router-dom";
// import CallApi from "../utils/callApi";

// const LibCardDetail = () => { 
//     const { id } = useParams();
//     const [detail, setDetail] = useState({});
//     const [history, setHistory] = useState([]);

//     useEffect(() => { 

//         const fetchApi = async () => { 
//             const resp = await CallApi('librarycards/' + id);
//             const data = resp.data;

//             setDetail(data.detail);
//             setHistory(data.history);
//         }

//         fetchApi();
//     }, [id])

//     return <div className="libcard-detail-page">
//         <div className="header">Thông tin thẻ: </div>
//         <div className="detail">
//             <div>mã: {detail.libraryCardID}</div>
//             <div className="name">tên: {detail.fullName}</div>
//             <div>ngày cấp: {detail.startDate}</div>
//             <div>hạn: {detail.expirationDate}</div>
//         </div>
//         <div className="header">Lịch sử mượn: </div>
//         <div className="history">
//             {history.map((x, ind) => <div className="item" key={ind}>
//                 {ind+1}/ mã: {x.lBookID}, ngày mượn: {x.startDate}, hạn: {x.deadline}, ngày trả: {x.endDate}, tình trạng sách: {x.bookStatus}
//             </div>)} 
//         </div>
//     </div>
// }

// export default LibCardDetail;