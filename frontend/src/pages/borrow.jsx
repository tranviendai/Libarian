import useFetch from '../utils/useFetch'
import DataTable from '../components/shared/data-table'
import { useEffect, useMemo, useRef, useState } from 'react';
import AddBorrowDialog from '../components/page/borrow/add-borrow';
import ReturnBookDialog from '../components/page/borrow/return-book';

const BorrowPage = () => { 

    const [page, setPage] = useState(1);
    const [borrow, setBorrow] = useState(true); //show only borrowed book
    const [searchId, setSearchId] = useState('');
    const [searchBook, setSearchBook] = useState('');
    const [searchCard, setSearchCard] = useState('');
    const [searchById, setSearchById] = useState(true);

    //dialogs
    const [showAddDialog, setShowAddDialog] = useState(false);
    const [selectedItem, setSelectedItem] = useState(null);
    const [showReturnDialog, setShowReturnDialog] = useState(false);
    const [borrows, setBorrows] = useState([]);
    const [pageCount, setPageCount] = useState(1);

    const borrowObj = useMemo(() => { 

        return {
            params: {
                page,
                returned: !borrow,
                id: searchId,
                bookID: searchBook,
                cardID: searchCard
            }
        }
    }, [page, borrow, searchId, searchBook, searchCard])
    const { data: borrowsData, loading } = useFetch('/callcards', borrowObj);
    useEffect(() => { 
        if (!borrowsData) return;

        setBorrows(borrowsData.list);
        setPageCount(borrowsData.pageCount);
    }, [borrowsData])

    const searchIdRef = useRef('');
    const searchBookRef = useRef('');
    const searchCardRef = useRef('');

    useEffect(() => { 
        setPage(1)
    }, [borrow, searchId])

    const headers = borrow ?
        ['Mã phiếu', 'Ngày mượn', 'Thời hạn', 'Mã sách', 'Mã thẻ']
        : ['Mã phiếu', 'Ngày mượn', 'Thời hạn', 'Ngày trả', 'Tình trạng sách', 'Mã sách', 'Mã thẻ'];
    const rows = borrows?.map(x => { 
        return {
            onRowSelected: () => { 
                setSelectedItem(x.borrow_id)
                setShowReturnDialog(true)
            },
            rowData: borrow ?
                [x.callCardID, x.startDate, x.deadline, x.lBookID, x.libraryCardID]
                :
                [x.callCardID, x.startDate, x.deadline, x.endDate || 'Chưa trả',
                    x.endDate ? x.bookStatus : 'Chưa trả', x.lBookID, x.libraryCardID]
        }
    })

    const onSearch = () => { 
        setSearchId(searchIdRef.current.value);
        setSearchBook(searchBookRef.current.value);
        setSearchCard(searchCardRef.current.value);
    }

    useEffect(() => { 
        if (searchById) {
            searchBookRef.current.value = '';
            searchCardRef.current.value = '';
        } else { 
            searchIdRef.current.value = '';
        }
    }, [searchById])

    const onInputEnter = (e) => { 
        if (e.key === 'Enter') onSearch();
    }

    return <div className="container-80 pb-3">
        <div className="page-title">Danh sách phiếu mượn - trả</div>

        <div className="mb-3">
            <button className="btn btn-primary" onClick={() => setShowAddDialog(true)}>Mượn sách</button>
        </div>

        {showAddDialog && <AddBorrowDialog onExit={() => setShowAddDialog(false)} />}
        {showReturnDialog && selectedItem && <ReturnBookDialog onExit={() => { setShowReturnDialog(false); }} id={selectedItem} />}

        <div className="mb-3">
            <div className="input-group me-2">
                <span className={`input-group-text btn ${searchById ? 'btn-primary' : 'btn-secondary'}`}
                    onClick={()=> setSearchById(true)}>Tìm mã phiếu</span>
                <input type="text" className="form-control me-5" placeholder='Nhập mã phiếu'
                    ref={searchIdRef} onKeyDown={onInputEnter} disabled={!searchById}/>
                
                <span className={`input-group-text btn ${!searchById ? 'btn-primary' : 'btn-secondary'}`}
                    onClick={() => setSearchById(false)}>Tìm mã sách</span>
                <input type="text" className="form-control" placeholder='Nhập mã sách'
                    ref={searchBookRef} onKeyDown={onInputEnter} disabled={searchById} />

                <span className={`input-group-text btn ${!searchById ? 'btn-primary' : 'btn-secondary'}`}
                    onClick={() => setSearchById(false)}>Tìm mã thẻ</span>
                <input type="text" className="form-control me-5" placeholder='Nhập mã thẻ'
                    ref={searchCardRef} onKeyDown={onInputEnter} disabled={searchById}/>

                <button className="btn btn-primary" onClick={onSearch}>Tra cứu</button>
            </div>
            
        </div>

        <div className='mb-3'>
            <label className={borrow ? 'btn btn-info me-3' : 'btn btn-outline-info me-3'} htmlFor='borrow'>Phiếu mượn</label>
            <input type="radio" id='borrow' name='borrow' checked={borrow} onChange={() => setBorrow(true)} hidden />
            <label className={!borrow ? 'btn btn-info' : 'btn btn-outline-info'} htmlFor='return'>Phiếu Trả</label>
            <input type="radio" id='return' name='borrow' checked={!borrow} onChange={() => setBorrow(false)} hidden />
        </div>

        {loading && <div className="loader"></div>}
        {(borrows && borrows.length > 0) ?
            <DataTable headers={headers} rows={rows} />
            : <p>Không tìm thấy kết quả nào</p>
        }
    
        <div>
            <input type="number" value={page} min={1} max={pageCount} onChange={(e) => setPage(e.target.value)} /> / {pageCount}
        </div>
    </div>
}

export default BorrowPage;