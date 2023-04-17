import { useEffect, useState } from 'react'
import CallApi from '../utils/callApi'
import FrmAddCate from '../components/page/category/frm-add-cate'
import useGlobalContext from '../contexts/GlobalContext'
import CategoriesGrid from '../components/shared/categories-grid'
import BG from '../resources/imgs/cate_bg.png'
import { useNavigate } from 'react-router-dom'

const { CategoriesData } = require('../mock-data')

const CategoryPage = () => {
    const [list, setList] = useState([]);
    const [showFrmAdd, setShowFrmAdd] = useState(false);
    const [refresh, setRefresh] = useState(false);
    const [selectedItem, setSelectedItem] = useState({});
    const [loading, setLoading] = useState(false);

    const { token } = useGlobalContext();

    const navigate = useNavigate();

    useEffect(() => {
        let mounted = true;

        const fetchApi = async () => {
            setLoading(true);
            try {
                const resp = await CallApi.get('/categories');
                const data = resp.data;
                if (mounted) setList(data);
            } catch (err) {
                console.log('api failed', err);
                if (mounted) setList(CategoriesData);
            } finally {
                setLoading(false);
            }
        }

        fetchApi();

        return () => {
            mounted = false
        }
    }, [refresh])

    const showFrm = () => {
        if (!token) return;
        setShowFrmAdd(true);
    }

    const onAdd = () => {
        setSelectedItem({});
        showFrm();
    }

    const selectCate = (id) => { 
        navigate('/LMS/Book', { state: { selectCate: id } });
    }

    return token ? <div className='category-page'>
        {showFrmAdd && <FrmAddCate setShow={setShowFrmAdd} item={selectedItem} setRefresh={setRefresh} />}
        <div className="container-80">
            <div className="header">
                <div className="title">Danh Sách Thể Loại</div>
                <div className="btn btn-add" onClick={onAdd}>
                    THÊM
                </div>
            </div>

            {loading && <div className="loader"></div>}
            <CategoriesGrid list={list} onSelect={(x) => { setSelectedItem(x); showFrm(); }} />
        </div>
    </div> : <div className="category-page">
            <div className="content">
                <div className="left">
                    <div className="header">
                        <div className="title">Các thể loại sách có trong B - Library</div>
                    </div>

                    <CategoriesGrid list={list} nCol={4} onSelect={(x) => selectCate(x.categoryID)} />
                </div>
                <div className="right">
                    <img src={BG} alt="" />
                </div>
            </div>
    </div>
}

export default CategoryPage;
