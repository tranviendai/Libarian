import { Link, useNavigate, useParams } from "react-router-dom";
import useFetch from "../utils/useFetch";
import DataTable from "../components/shared/data-table";
import { CallApiWithToken } from "../utils/callApi";
import useGlobalContext from "../contexts/GlobalContext";
import { useEffect, useState } from "react";
import { SearchIcon } from "../utils/icons";

const LibCardDetail = () => {
    const { id } = useParams();

    const { token } = useGlobalContext();
    const navigate = useNavigate();

    const [card, setCard] = useState({});
    const [history, setHistory] = useState([]);
    const [userId, setUserId] = useState('');

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

    const { data: finesData } = useFetch('/PenaltyTickets?cardId=' + id);
    const headersFine = ['Phiếu mượn', 'Lỗi', 'Số tiền', 'Tình trạng'];
    const rowsFine = finesData?.list.map(x => {
        return {
            onRowSelected: () => {},
            rowData: [
                x.callCardID, x.reason, x.price?.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + 'đ',
                x.status?'Đã trả':'Chưa trả'
            ]
        }
    })

    const blockCard = async () => {
        try {
            await CallApiWithToken(token).delete('/LibraryCards/' + id);
            window.location.reload();
        } catch {
            alert('Failed')
        }
    }

    const onHistoryKeyDown = (e) => {
        if (e.key === 'Enter') {
            searchHistory();
        }
    }
    const searchHistory = () => {
        if (!userId) return;
        navigate('/BLibrary/LibCard/' + userId);
    }

    if (!card.libraryCardID) return <div className="container-80 card-detail-page">
        <div className="search-history search mt-5">
            <label htmlFor="history">ID</label>
            <input type="text" id="history" className="pill" placeholder="Nhập ID"
                onKeyDown={(e) => onHistoryKeyDown(e)} onChange={(e) => { setUserId(e.target.value) }} value={userId} />
            <div className="btn" onClick={searchHistory}>
                <SearchIcon />
            </div>
        </div>

        <h3 className="mt-5 mb-5"><center>Không tìm thấy kết quả "{id}"</center></h3>
    </div>
    return <div className="container-80">
        <div className="page-title">Thông tin thẻ</div>

        {loading && <div className="loader"></div>}
        {card.libraryCardID && <div className="mx-auto col-8">
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
        <div className="mb-3"></div>{(history && history.length > 0) ? <DataTable headers={headers} rows={rows} noEdit /> : <p><i>Không có lịch sử mượn</i></p>}

        <div className="page-title">Lịch sử phạt</div>
        <div>
            {finesData && finesData.list.length > 0 ?
                <DataTable rows={rowsFine} headers={headersFine} noEdit />
                : <p><i>Không có lịch sử phạt</i></p>}
        </div>
    </div>
}

export default LibCardDetail;