import useFetch from '../utils/useFetch'
import DataTable from '../components/shared/data-table';
import { useEffect, useMemo, useRef, useState } from 'react';
import PayFineDialog from '../components/page/fine/pay_fine';

const FinePage = () => { 

    const [page, setPage] = useState(1);
    const [paid, setPaid] = useState(false);
    const [cardId, setCardId] = useState('');
    const [borrowId, setBorrowId] = useState('');
    const [searchByBorrow, setSearchByBorrow] = useState(true);
    const [selectedItem, setSelectedItem] = useState({});
    const [showDialog, setShowDialog] = useState(false);
    const [refresh, setRefresh] = useState(false);

    const [fines, setFines] = useState([]);
    const [pageCount, setPageCount] = useState(1);

    const searchBorrowRef = useRef('');
    const searchCardRef = useRef('');

    const fineObj = useMemo(() => {
        return {
            params: {
                page,
                paid,
                cardId,
                borrowId,
                refresh
            }
        }
    }, [page, paid, cardId, borrowId, refresh]);
    const { data: finesData, loading } = useFetch('/PenaltyTickets', fineObj);

    useEffect(() => { 
        if (!finesData) return;
        setFines(finesData.list);
        setPageCount(finesData.pageCount);

    }, [finesData])

    useEffect(() => { 
        setPage(1);
    }, [paid, cardId, borrowId, refresh])


    const headers = ['Phiếu mượn', 'Mã thẻ' ,'Lỗi', 'Số tiền'];
    const rows = fines?.map(x => { 
        return {
            onRowSelected: () => { 
                setSelectedItem(x);
                setShowDialog(true);
            },
            rowData: [x.callCardID, x.libraryCardID, x.reason, x.price?.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + 'đ']
        }
    })

    const onSearch = () => { 
        setCardId(searchCardRef.current.value);
        setBorrowId(searchBorrowRef.current.value);
    }

    const onInputEnter = (event) => {
        if (event.key === 'Enter') {
            onSearch();
        }
    }

    useEffect(() => { 
        if (searchByBorrow) {
            searchCardRef.current.value = '';
        } else { 
            searchBorrowRef.current.value = '';
        }
    }, [searchByBorrow])


    return <div className="container-80 pb-5">
        <div className="page-title">Danh sách phiếu phạt</div>
        {showDialog && <PayFineDialog onExit={() => setShowDialog(false)} item={selectedItem} refresh={()=>setRefresh(x => !x)} />}

        <div className="input-group mb-4">
            <div className={`btn me-3 ${!paid ? 'btn-info' : 'btn-outline-info'}`} onClick={() => setPaid(false)}>Chưa trả</div>
            <div className={`btn me-5 ${paid ? 'btn-info' : 'btn-outline-info'}`} onClick={() => setPaid(true)}>Đã trả</div>

            <span className={`input-group-text btn ${searchByBorrow ? 'btn-primary' : 'btn-secondary'}`} id="basic-addon1" onClick={() => setSearchByBorrow(true)}>Tìm theo mã phiếu</span>
            <input type="text" className="form-control me-4" placeholder="Mã phiếu mượn" aria-label="Username" aria-describedby="basic-addon1"
                ref={searchBorrowRef} onKeyDown={(e) => onInputEnter(e)} disabled={!searchByBorrow} />
            <span className={`input-group-text btn ${!searchByBorrow ? 'btn-primary' : 'btn-secondary'}`} id="basic-addon1" onClick={() => setSearchByBorrow(false)}>Tìm theo mã thẻ</span>
            <input type="text" className="form-control me-4" placeholder="Mã thẻ" aria-label="Username" aria-describedby="basic-addon1"
                ref={searchCardRef} onKeyDown={(e) => onInputEnter(e)} disabled={searchByBorrow} />
            <button onClick={onSearch} className="btn btn-primary">Tra cứu</button>
        </div>
        {loading ? <div className="loader"></div> :
            (fines && fines.length) ? <DataTable headers={headers} rows={rows} noEdit={paid} /> : <p className='mb-3'><i>Không tìm thấy</i></p>}
        <div className="pager">
            Trang <input type="number" max={pageCount} min={1} value={page} onChange={(e) => setPage(e.currentTarget.value)} /> / {pageCount}
        </div>
    </div>
}

export default FinePage;