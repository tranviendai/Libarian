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

        {(history && history.length > 0) ? <DataTable headers={headers} rows={rows} noEdit /> : <h3>Không có lịch sử mượn</h3>}
    </div>
}

export default LibCardDetail;