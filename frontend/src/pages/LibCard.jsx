import { useEffect, useRef, useState } from "react";
import { CallApiWithToken } from "../utils/callApi";
import useGlobalContext from "../contexts/GlobalContext";
import { Link } from "react-router-dom";

const LibCard = () => { 
    const [Cards, setCards] = useState([]);
    const [pageCount, setPageCount] = useState(1);
    const [page, setPage] = useState(1);
    const [searchName, setSearchName] = useState('');
    const [searchID, setSearchID] = useState('');

    //inputs control
    const searchNameRef = useRef('');
    const searchIDRef = useRef('');

    const { token } = useGlobalContext();

    useEffect(() => { 

        const fetchApi = async () => { 
            try {
                const resp = await CallApiWithToken(token).get('/libraryCards', {
                    params: {
                        page,
                        searchName,
                        searchID
                    }
                });
                const data = resp.data;
                setPageCount(data.pageCount);
                setCards(data.list);
            } catch (err) { 
                console.log(err);
            }
        }

        fetchApi();

    }, [token, page, searchName, searchID])

    const onSearch = () => { 
        setSearchID(searchIDRef.current.value);
        setSearchName(searchNameRef.current.value);
    }

    return <div className="libcard-page">
        <div className="header">Thẻ thư viện: </div>
        <div className="search">
            <div>
                <label htmlFor="">Tên</label>
                <input type="text" ref={searchNameRef} />
            </div>
            <div>
                <label htmlFor="">Mã thẻ</label>
                <input type="text" ref={searchIDRef} />
            </div>
            <button onClick={onSearch}>Search</button>
        </div>
        <div className="card-list">
            {Cards.map(x => <div className="libcard" key={x.libraryCardID}>
                <Link to={`${x.libraryCardID}`}>mã: {x.libraryCardID}, tên: <b>{x.fullName}</b>, trạng thái: {x.cardStatus}, ngày cấp: {x.startDate}, hạn: {x.expirationDate}</Link>
            </div>)}
        </div>
        <div className="pager">
            <input type="number" min={1} max={pageCount} value={page} onChange={e => setPage(e.target.value)} /> / {pageCount}
        </div>
    </div>
}

export default LibCard;