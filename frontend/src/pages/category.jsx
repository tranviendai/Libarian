import { useEffect, useState } from 'react'
import CallApi from '../utils/callApi'
import FrmAddCate from '../components/page/category/frm-add-cate'

const { CategoriesData } = require('../mock-data')

const CategoryPage = () => { 
    const [list, setList] = useState([]);
    const [showFrmAdd, setShowFrmAdd] = useState(false);
    const [refresh, setRefresh] = useState(false);
    const [selectedItem, setSelectedItem] = useState({});
    const [loading, setLoading] = useState(false);

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
        setShowFrmAdd(true);
    }

    const onAdd = () => { 
        setSelectedItem({});
        showFrm();
    }

    return <div className='category-page'>
        {showFrmAdd && <FrmAddCate setShow={setShowFrmAdd} item={selectedItem} setRefresh={setRefresh} />}
        <h2 className="title">Danh Sách thể loại</h2>
        <div className='container-80'>
            <div className="btn-add btn" onClick={onAdd}>+ Thêm thể loại</div>
            
                {loading ?
                    <div className="loader"></div>
                    : <>
                        <p><i>Click vào thể loại để chỉnh sửa</i></p>
                        <div className="category-list ">
                        {list.map(x => <div key={x.categoryID} className='category-item btn' onClick={() => { setSelectedItem(x); showFrm(); }} >
                                <div className="name">{x.nameCategory}</div>
                            </div>)}
                        </div>
                    </>
                }
        </div>
    </div>
}

export default CategoryPage;